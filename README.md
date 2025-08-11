# Mliybs.QQBot

.NET实现腾讯QQ机器人SDK，基于.NET8与.NET Standard 2.1

# 使用

> 这是官方QQ机器人的.NET SDK，使用前请保证你已成功申请了官方QQ机器人

使用QQBot前需要知道QQ机器人的id和secret，这些都可以在机器人管理端查询到（secret只有机器人管理员能刷新与查看）

> 目前还没有支持webhook方式，之后会逐渐支持

## WebSocket方式

### 控制台方式

```CSharp
using var bot = new WebSocketBot(id, secret); // 此处的id与secret为外部填入的参数

// 该方法会返回ReadyEvent对象，只要没有抛出异常就视为连接成功
// 方法参数填入需要订阅的事件，默认不填表示订阅私信与群聊事件（不包括频道私信）
// 使用位或运算订阅多个事件，如WebSocketIntent.GroupAndC2cEvent | WebSocketIntent.PublicGuildMessages
await bot.ConnectAsync();

bot.MessageReceived.Subscribe(async x =>
{
    // 回复消息
})

bot.KeepRunning(); // 如果当前线程为主线程请调用该方法或Console.ReadLine()卡住线程
```

# BUG

有BUG请在issues提出，有能力帮助可以发起pull request

# 赞助

如果你喜欢此项目并有意向赞助我的开发，请参考[爱发电](https://afdian.com/a/mliybs)

铁子来一杯吗(´・ω・)つ旦