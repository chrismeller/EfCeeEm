using EfCeeEmSharp.Client;
using EfCeeEmSharp.Config;
using EfCeeEmSharp.Thread.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EfCeeEmSharp.Thread.Consumers;

public class GetThreadsConsumer : IConsumer<GetThreads>
{
    private readonly ILogger<GetThreadsConsumer> _logger;
    private readonly IOptions<AppSettings> _options;
    private readonly FourChanClient _client;

    public GetThreadsConsumer(ILogger<GetThreadsConsumer> logger, IOptions<AppSettings> options, FourChanClient client)
    {
        _logger = logger;
        _options = options;
        _client = client;
    }

    public async Task Consume(ConsumeContext<GetThreads> context)
    {
        _logger.LogDebug("Received GetThreads for board {Board}", context.Message.Board);

        // get the last etag and modified dates we have stored



        // @todo make sure that all HTTP requests are done via a single endpoint so we can _ensure_ that the 1 req/sec limit isn't exceeded
        // @todo also include some logic for making requests through different proxies


        if (await _client.GetThreadListHasChanged(context.Message.Board))
        {

        }
        else
        {
            _logger.LogDebug("Thread list for board {Board} has not changed since last check", context.Message.Board);
        }

        _logger.LogDebug("Completed GetThreads for board {Board}", context.Message.Board);
    }
}