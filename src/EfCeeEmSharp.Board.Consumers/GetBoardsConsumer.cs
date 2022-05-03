using System.Net.Http.Json;
using EfCeeEmSharp.Board.Contracts;
using EfCeeEmSharp.Client;
using EfCeeEmSharp.Config;
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

        var boardsToRun = "";
        if (!string.IsNullOrEmpty(_options.Value.BoardsToRun))
        {
            boardsToRun = _options.Value.BoardsToRun;
        }
        else
        {
            // note that this is very bad. there's no reason to re-poll every time we start
            // but there's no point in storing this somewhere - you should just be specifying the list!
            var boards = await _client.GetBoards();

            boardsToRun = String.Join(",", boards.Data.Select(x => x.Name));
        }

        foreach (var board in boardsToRun.Split(","))
        {
            // await context.Send();
        }
        
        _logger.LogDebug("Completed GetBoards");
    }
}