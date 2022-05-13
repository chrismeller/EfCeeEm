// See https://aka.ms/new-console-template for more information

using System.Reflection;
using EfCeeEmSharp.Board.Consumer;
using EfCeeEmSharp.Client;
using EfCeeEmSharp.Config;
using EfCeeEmSharp.Host;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.Configure<AppSettings>(hostContext.Configuration.GetSection("App"));

        services.AddSingleton<FourChanClient>();

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            x.SetInMemorySagaRepositoryProvider();

            var entryAssembly = Assembly.GetEntryAssembly();

            x.AddConsumers(entryAssembly);
            x.AddSagaStateMachines(entryAssembly);
            x.AddSagas(entryAssembly);
            x.AddActivities(entryAssembly);

            x.AddConsumers(typeof(GetBoardsConsumer).Assembly);

            x.UsingRabbitMq((context, cfg) =>
            {
                var config = context.GetRequiredService<IOptions<AppSettings>>();

                cfg.Host(config.Value.RabbitMq.Hostname, config.Value.RabbitMq.Vhost, h =>
                {
                    h.Username(config.Value.RabbitMq.Username);
                    h.Password(config.Value.RabbitMq.Password);
                });

                cfg.ConfigureEndpoints(context);
            });

            x.AddHostedService<App>();
        });
    })
    .Build()
    .RunAsync();