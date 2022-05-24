using System.Reflection;
using EfCeeEmSharp.Client;
using EfCeeEmSharp.Config;
using EfCeeEmSharp.Thread.Consumers;
using EfCeeEmSharp.Thread.Contracts;
using EfCeeEmSharp.Thread.Data;
using EfCeeEmSharp.Thread.Domain;
using EfCeeEmSharp.Thread.Host;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.Configure<AppSettings>(hostContext.Configuration.GetSection("App"));

        services.AddSingleton<FourChanClient>();

        // @todo move this to a module
        services.AddTransient<IThreadQueryService, ThreadQueryService>();
        services.AddDbContext<ThreadDbContext>(options =>
                options.UseSqlServer(hostContext.Configuration.GetConnectionString("ThreadDbContext")),
            ServiceLifetime.Transient);

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            x.SetInMemorySagaRepositoryProvider();

            var entryAssembly = Assembly.GetEntryAssembly();

            x.AddConsumers(entryAssembly);
            x.AddSagaStateMachines(entryAssembly);
            x.AddSagas(entryAssembly);
            x.AddActivities(entryAssembly);

            x.AddConsumers(typeof(GetThreadsConsumer).Assembly);

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