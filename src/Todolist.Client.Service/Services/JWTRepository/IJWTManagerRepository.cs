namespace TodoList.Service.Services.JWTRepository;

public interface IJWTManagerRepository
{
    public Tokens Authenticate(Users users);
    public string ReadClaims(string token, string targetClaimType);


}