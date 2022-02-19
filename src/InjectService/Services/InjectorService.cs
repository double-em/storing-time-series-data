using Grpc.Core;
using InjectApi;

namespace InjectService.Services;

public class InjectorService : Injector.InjectorBase
{
    private readonly ILogger<InjectorService> _logger;

    public InjectorService(ILogger<InjectorService> logger)
    {
        _logger = logger;
    }

    public override Task<MetricReply> Insert(MetricRequest request, ServerCallContext context)
    {
        return Task.FromResult(new MetricReply
        {
            Message = $"[{request.Name}] {request.Value}"
        });
    }
}