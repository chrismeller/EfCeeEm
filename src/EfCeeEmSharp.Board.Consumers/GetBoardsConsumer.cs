using EfCeeEmSharp.Board.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace EfCeeEmSharp.Board.Consumers;

public class GetBoardsConsumer : IConsumer<GetBoards>
{
    private readonly ILogger<GetBoardsConsumer> _logger;

    public GetBoardsConsumer(ILogger<GetBoardsConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<GetBoards> context)
    {
        _logger.LogInformation("Received GetBoards");
        return Task.CompletedTask;
    }
}