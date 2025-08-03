using System;
using System.Buffers;
using System.IO;
using System.IO.Pipelines;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Mliybs.QQBot.Buffers;

public class WebSocketPipeReader : IDisposable
{
    private readonly Pipe pipe = new Pipe(new(useSynchronizationContext: false));

    public async Task<ReadOnlySequence<byte>> ReadToEndAsync(ClientWebSocket webSocket)
    {
        ArgumentNullException.ThrowIfNull(webSocket);

        // 启动写入任务
        var writing = FillPipeAsync(webSocket, pipe.Writer);
        // 启动读取任务
        var reading = ReadPipeAsync(pipe.Reader);

        // 等待两个任务完成
        await Task.WhenAll(writing, reading);

        return reading.Result;
    }

    private static async Task FillPipeAsync(ClientWebSocket webSocket, PipeWriter writer)
    {
        while (true)
        {
            // 从PipeWriter获取内存
            var memory = writer.GetMemory();

            // 从流中读取数据
            var result = await webSocket.ReceiveAsync(memory, CancellationToken.None).ConfigureAwait(false);

            var bytesRead = result.Count;

            if (result.MessageType == WebSocketMessageType.Close) throw new InvalidDataException();

            if (result.EndOfMessage)
            {
                writer.Advance(bytesRead);
                await writer.FlushAsync().ConfigureAwait(false);
                break;
            }

            // 告诉PipeWriter我们已经写入了多少数据
            writer.Advance(bytesRead);

            // 使数据对PipeReader可用
            var flush = await writer.FlushAsync().ConfigureAwait(false);

            if (flush.IsCompleted)
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

    ~WebSocketPipeReader()
    {
        pipe.Reader.Complete();
    }
}