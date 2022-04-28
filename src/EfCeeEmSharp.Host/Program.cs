// See https://aka.ms/new-console-template for more information

using System.Reflection;
using EfCeeEmSharp.Board.Consumers;
using EfCeeEmSharp.Host;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.Configure<AppSettings>(hostContext.Configuration.GetSection("App"));
        
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
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("admin");
                    h.Password("password");
                });
                
                cfg.ConfigureEndpoints(context);
            });

            x.AddHostedService<App>();
        });
    })
    .Build()
    .RunAsync();