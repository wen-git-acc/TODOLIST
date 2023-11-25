namespace TodoList.Service.Services.JWTRepository;

public interface IJWTManagerRepository
{
    Tokens Authenticate(Users users);
}