using TodoList.Service.Schema;

namespace TodoList.Service.Services;

public interface ITaskRepository
{
    public TaskItemConfig GetTaskItems();
    public List<TaskItemConfig> GetTasksByFiltering(string? status, DateTime? dueDate);
    public List<TaskItemConfig> GetTasksBySorting(string? status, string? name, DateTime? dueDate);
}