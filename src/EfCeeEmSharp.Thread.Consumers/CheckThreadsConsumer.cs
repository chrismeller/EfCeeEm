using EfCeeEmSharp.Client;
using EfCeeEmSharp.Thread.Contracts;
using Microsoft.Extensions.Logging;

namespace EfCeeEmSharp.Thread.Consumers;

public class CheckThreadsConsumer
{
    private readonly ILogger<CheckThreadsConsumer> _logger;
    private readonly FourChanClient _client;
    private readonly IThreadQueryService _queryService;
}