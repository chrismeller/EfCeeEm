using EfCeeEmSharp.Board.Contracts;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace EfCeeEmSharp.Host;

public class App : BackgroundService
{
    private readonly IBus _bus;

    public App(IBus bus)
    {
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _bus.Publish(new GetBoards(), stoppingToken);

            await Task.Delay(1000, stoppingToken);
        }
    }
}