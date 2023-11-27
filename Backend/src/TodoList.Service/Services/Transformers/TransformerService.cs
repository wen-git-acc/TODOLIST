using TodoList.Service.Schema;
using TodoList.Service.Services.JWTRepository;
using TaskStatus = TodoList.Service.Schema.TaskStatus;

namespace TodoList.Service.Services.Transformers
{
    public class TransformerService : ITransformerService
    {
        private readonly IJWTManagerRepository _jwtManagerRepository;

        public TransformerService(IJWTManagerRepository jwtManagerRepository)
        {
            _jwtManagerRepository = jwtManagerRepository;
        }
        public string GetUserFromClaims(HttpRequest request, string targetClaimType)
        {
            var token = request.Headers.Authorization;
            if (String.IsNullOrEmpty(token))
            {
                throw new KeyNotFoundException("Token not found");

            }
            var targetClaim = _jwtManagerRepository.ReadClaims(token!, targetClaimType);

            return targetClaim;
        }

        public FirestoreTaskItemConfig TransformToFireStoreConfig(TaskItemConfig data, List<string> users)
        {
            var firebaseData = new FirestoreTaskItemConfig
            {
                DocumentId = data.UniqueId,
                DueDate = data.DueDate,
                Name = data.Name,
                Owner = users,
                Status = data.Status.ToString(),
                UniqueId = data.UniqueId,
                Description = data.Description,
            };

            return firebaseData;
        }

        public TaskItemConfig TransformToTaskItemConfig(FirestoreTaskItemConfig data)
        {
            var taskItem = new TaskItemConfig
            {
                UniqueId = data.UniqueId,
                Description = data.Description,
                DueDate = data.DueDate,
                Name = data.Name,
                Status = Enum.TryParse(data.Status.ToLower(), out TaskStatus value) ? value : null,
            };

            return taskItem;
        }
    }
}

