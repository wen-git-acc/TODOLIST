using TodoList.Service.Services.JWTRepository;

namespace TodoList.Service.Services.Transformers
{
    public class TransformerService : ITransformerService
    {
        private readonly IJWTManagerRepository _jwtManagerRepository;

        public TransformerService(IJWTManagerRepository jwtManagerRepository)
        {
            _jwtManagerRepository = jwtManagerRepository;
        }
        public string GetUserNameFromClaims(HttpRequest request, string targetClaimType)
        {
            var token = request.Headers.Authorization;
            if (String.IsNullOrEmpty(token))
            {
                throw new KeyNotFoundException("Token not found");

            }
            var targetClaim = _jwtManagerRepository.ReadClaims(token!, targetClaimType);

            return targetClaim;
        }
    }
}
