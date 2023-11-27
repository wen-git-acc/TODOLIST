namespace TodoList.Service.Services.Database
{
    public interface IDatabaseService
    {
        public string GetUserFromAuth(HttpRequest request, string targetClaimType);
    }
}
