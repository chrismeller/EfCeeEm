using System.Net.Http.Json;
using EfCeeEmSharp.Board.Consumer.DTOs;
using EfCeeEmSharp.Board.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace EfCeeEmSharp.Board.Consumer;

public class GetBoardsConsumer : IConsumer<GetBoards>
{
    private readonly ILogger<GetBoardsConsumer> _logger;

    public GetBoardsConsumer(ILogger<GetBoardsConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<GetBoards> context)
    {
        _logger.LogDebug("Received GetBoards");

        using var http = new HttpClient();
        var response = await http.GetFromJsonAsync<BoardsResponse>("https://a.4cdn.org/boards.json");
    }
}