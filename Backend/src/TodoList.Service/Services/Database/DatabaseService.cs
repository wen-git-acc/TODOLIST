using Google.Rpc.Context;
using TodoList.Service.Services.Transformers;

namespace TodoList.Service.Services.Database;

public class DatabaseService : IDatabaseService
{
    private readonly ITransformerService _transformerService;

    public DatabaseService(ITransformerService transformerService)
    {
        _transformerService = transformerService;
    }

    public string GetUserFromAuth(HttpRequest request, string targetClaimType)
    {
        return _transformerService.GetUserFromClaims(request, targetClaimType);
    }
    
}