using EfCeeEmSharp.Client;
using EfCeeEmSharp.Config;
using EfCeeEmSharp.Post.Contracts.Commands;
using EfCeeEmSharp.Thread.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EfCeeEmSharp.Thread.Consumers;

public class GetThreadsConsumer : IConsumer<GetThreads>
{
    private readonly ILogger<GetThreadsConsumer> _logger;
    private readonly FourChanClient _client;
    private readonly IThreadQueryService _queryService;

    public GetThreadsConsumer(ILogger<GetThreadsConsumer> logger, FourChanClient client, IThreadQueryService queryService)
    {
        _logger = logger;
        _client = client;
        _queryService = queryService;
    }

    public async Task Consume(ConsumeContext<GetThreads> context)
    {
        _logger.LogDebug("Received GetThreads for board {Board}", context.Message.Board);

        var mostRecentThread = await _queryService.GetMostRecentThreadAsync(context.Message.Board);

        // if we don't have any threads stored, or the list has been modified since the most recent one was checked
        if (mostRecentThread == null || await _client.GetThreadListHasChanged(context.Message.Board, mostRecentThread?.ETag,
                mostRecentThread?.LastModifiedAt))
        {
            // there's new content to get!
            _logger.LogDebug("Thread list for board {Board} has changed since last check and is being fetched", context.Message.Board);

            var threads = await _client.GetThreadList(context.Message.Board);

            foreach (var page in threads.Data)
            {
                foreach (var thread in page.Threads)
                {
                    await context.Send<GetPosts>(new
                    {
                        Board = context.Message.Board,
                        Thread = thread.Number,
                    });
                }
            }
        }
        else
        {
            _logger.LogDebug("Thread list for board {Board} has not changed since last check", context.Message.Board);
        }

        _logger.LogDebug("Completed GetThreads for board {Board}", context.Message.Board);
    }
}