using MassTransit;

namespace EfCeeEmSharp.Thread.Consumers;

public class GetThreadsConsumerDefinition : ConsumerDefinition<GetThreadsConsumer>
{
    public GetThreadsConsumerDefinition()
    {
        ConcurrentMessageLimit = 5;
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<GetThreadsConsumer> consumerConfigurator)
    {
        // configure message retry with millisecond intervals
        endpointConfigurator.UseMessageRetry(r => r.Intervals(100, 200, 500, 800, 1000));

        // use the outbox to prevent duplicate events from being published
        endpointConfigurator.UseInMemoryOutbox();
    }
}