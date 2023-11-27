using Microsoft.AspNetCore.Mvc;
using TodoList.Service.Schema;

namespace TodoList.Service.Services.Database
{
    public interface IDatabaseService
    {
        public string GetUserFromAuth(HttpRequest request, string targetClaimType);
        public Task<ActionResult<List<TaskItemConfig>>> CreateNewItem(TaskItemConfig taskItem, string user);
        public Task<ActionResult<List<TaskItemConfig>>> GetTaskItems(string user);
        public Task<ActionResult<List<TaskItemConfig>>> DeleteItem(TaskItemConfig taskItem, string user);

        public Task<ActionResult<List<TaskItemConfig>>> UpdateItem(TaskItemConfig taskItem, string user, string? newItemUser);

        public Task<ActionResult<List<TaskItemConfig>>> GetTaskItemsByFilterSort(string user, string? status, string? name, DateTime? dueDate, string? order);
    }
}
