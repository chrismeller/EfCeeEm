using EfCeeEmSharp.Board.Contracts;
using EfCeeEmSharp.Client;
using EfCeeEmSharp.Config;
using EfCeeEmSharp.Thread.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EfCeeEmSharp.Board.Consumer;

public class GetBoardsConsumer : IConsumer<GetBoards>
{
    private readonly ILogger<GetBoardsConsumer> _logger;
    private readonly IOptions<AppSettings> _options;
    private readonly FourChanClient _client;

    public GetBoardsConsumer(ILogger<GetBoardsConsumer> logger, IOptions<AppSettings> options, FourChanClient client)
    {
        _logger = logger;
        _options = options;
        _client = client;
    }

    public async Task Consume(ConsumeContext<GetBoards> context)
    {
        _logger.LogDebug("Received GetBoards");


        if (string.IsNullOrEmpty(_options.Value.BoardsToRun))
        {
            throw new Exception("No boards were specified to run. Update your config!");
        }

        var boardsToRun = _options.Value.BoardsToRun.Split(',');

        foreach (var board in boardsToRun)
        {
            // await context.Send();
        }

        _logger.LogDebug("Completed GetBoards");
    }
}