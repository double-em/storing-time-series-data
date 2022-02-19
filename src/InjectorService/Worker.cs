using Grpc.Net.Client;
using InjectApi;

namespace InjectorService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var tasks = new List<Thread>();
        
        for (var i = 0; i < 1000; i++)
        {
            var jobId = i;
            var thread = new Thread(async() => await PostJob(jobId, stoppingToken));
            tasks.Add(thread);
        }

        foreach (var task in tasks)
        {
            task.Start();
        }
        
        return Task.CompletedTask;
    }

    private async Task PostJob(int id, CancellationToken stoppingToken)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:5000", new GrpcChannelOptions
        {
            HttpHandler = new SocketsHttpHandler
            {
                EnableMultipleHttp2Connections = true
            }
        });
        var client = new Injector.InjectorClient(channel);

        while (!stoppingToken.IsCancellationRequested)
        {
            var delay = Random.Shared.Next(100, 1000);
            // _logger.LogInformation("{Worker} delay: {Time}ms", id, delay);
            
            await Task.Delay(delay, stoppingToken);
            
            var reply = await client.InsertAsync(
                new MetricRequest
                {
                    Name = id.ToString(),
                    Value = Random.Shared.NextInt64(69420)
                },
                cancellationToken: stoppingToken);
            
            // _logger.LogInformation(reply.Message);
        }
    }
}