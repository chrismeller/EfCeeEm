using System.Linq;
using System.Threading.Tasks;
using EfCeeEmSharp.Board.Consumer;
using EfCeeEmSharp.Board.Contracts;
using EfCeeEmSharp.Client;
using EfCeeEmSharp.Config;
using MassTransit.Testing;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace EfCeeEmSharp.Board.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ShouldConsumeMessage()
        {
            var harness = new InMemoryTestHarness();
            var consumer = harness.Consumer(() => new GetBoardsConsumer(new NullLogger<GetBoardsConsumer>(),
                new OptionsWrapper<AppSettings>(new AppSettings() { BoardsToRun = "b" }), new FourChanClient()));

            await harness.Start();

            try
            {
                await harness.InputQueueSendEndpoint.Send(new GetBoards());

                Assert.That(consumer.Consumed.Select<GetBoards>().Any(), Is.True);
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}