using MassTransit;

namespace EfCeeEmSharp.Board.Consumers;

public class GetBoardsConsumerDefinition : ConsumerDefinition<GetBoardsConsumer>
{
    public GetBoardsConsumerDefinition()
    {
        ConcurrentMessageLimit = 10;
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<GetBoardsConsumer> consumerConfigurator)
    {
        // configure message retry with millisecond intervals
        endpointConfigurator.UseMessageRetry(r => r.Intervals(100,200,500,800,1000));

        // use the outbox to prevent duplicate events from being published
        endpointConfigurator.UseInMemoryOutbox();
    }
}