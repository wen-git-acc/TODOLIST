using TodoList.Service.Schema;

namespace TodoList.Service.Services.Transformers
{
    public interface ITransformerService
    {
        public string GetUserFromClaims(HttpRequest request, string targetClaimType);
        public FirestoreTaskItemConfig TransformToFireStoreConfig(TaskItemConfig taskItem, List<string> users);
        public TaskItemConfig TransformToTaskItemConfig(FirestoreTaskItemConfig data);
    }
}
