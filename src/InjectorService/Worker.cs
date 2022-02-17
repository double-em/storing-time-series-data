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

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var channel = GrpcChannel.ForAddress("https://injectservice:7000");
        var client = new Greeter.GreeterClient(channel);

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
            
            var reply = await client.SayHelloAsync(
                new HelloRequest { Name = "GreeterClient" },
                cancellationToken: stoppingToken);
            
            _logger.LogInformation(reply.Message);
        }
    }
}