namespace TodoList.Service.Services.Transformers
{
    public interface ITransformerService
    {
        public string GetUserFromClaims(HttpRequest request, string targetClaimType);
    }
}
