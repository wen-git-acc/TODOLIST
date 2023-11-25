using TodoList.Service.Schema;

namespace TodoList.Service.Services
{
    public class TaskRepository : ITaskRepository
    {
        public TaskItemConfig GetTaskItems()
        {
            throw new NotImplementedException();
        }

        public List<TaskItemConfig> GetTasksByFiltering(string? status, DateTime? dueDate)
        {
            throw new NotImplementedException();
        }

        public List<TaskItemConfig> GetTasksBySorting(string? status, string? name, DateTime? dueDate)
        {
            throw new NotImplementedException();
        }
    }
}
