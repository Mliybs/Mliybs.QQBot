using System;
using System.Buffers;
using System.IO;
using System.IO.Pipelines;
using System.Threading.Tasks;

namespace Mliybs.QQBot.Buffers;

public class StreamPipeReader : IDisposable
{
    private readonly Pipe pipe = new Pipe(new(useSynchronizationContext: false));

    public async Task<ReadOnlySequence<byte>> ReadToEndAsync(Stream stream, OnRead? onRead = null)
    {
        ArgumentNullException.ThrowIfNull(stream);

        // 启动写入任务
        var writing = FillPipeAsync(stream, pipe.Writer, onRead);
        // 启动读取任务
        var reading = ReadPipeAsync(pipe.Reader);

        // 等待两个任务完成
        await Task.WhenAll(writing, reading);

        return reading.Result;
    }

    private static async Task FillPipeAsync(Stream stream, PipeWriter writer, OnRead? onRead = null)
    {
        while (true)
        {
            // 从PipeWriter获取内存
            var memory = writer.GetMemory();

            // 从流中读取数据
            var bytesRead = await stream.ReadAsync(memory).ConfigureAwait(false);

            if (bytesRead == 0)
            {
                await writer.FlushAsync().ConfigureAwait(false);
                break;
            }

            onRead?.Invoke(memory.Span.Slice(0, bytesRead));

            // 告诉PipeWriter我们已经写入了多少数据
            writer.Advance(bytesRead);

            // 使数据对PipeReader可用
            var result = await writer.FlushAsync().ConfigureAwait(false);

            if (result.IsCompleted)
                break;
        }

        // 告诉PipeWriter我们已经完成写入
        await writer.CompleteAsync().ConfigureAwait(false);
    }

    private static async Task<ReadOnlySequence<byte>> ReadPipeAsync(PipeReader reader)
    {
        while (true)
        {
            // 从PipeReader中读取数据
            var result = await reader.ReadAsync().ConfigureAwait(false);
            var buffer = result.Buffer;

            reader.AdvanceTo(buffer.Start, buffer.End);

            if (result.IsCompleted) return buffer;
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        pipe.Reader.Complete();
    }

    ~StreamPipeReader()
    {
        pipe.Reader.Complete();
    }

    public delegate void OnRead(ReadOnlySpan<byte> span);
}