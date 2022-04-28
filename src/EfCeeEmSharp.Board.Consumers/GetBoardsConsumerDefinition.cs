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
        
    }
}