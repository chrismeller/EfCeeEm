using MassTransit;
using Microsoft.Extensions.Hosting;

namespace EfCeeEmSharp.Thread.Host;

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
        }
    }
}