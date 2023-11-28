using System.Net;
using Google.Rpc.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using TodoList.Service.Extensions;
using TodoList.Service.Schema;
using TodoList.Service.Services.Transformers;

namespace TodoList.Service.Services.Database;

public class DatabaseService : IDatabaseService
{
    private readonly ITransformerService _transformer;
    private readonly IFirestoreService _firestoreDb;

    public DatabaseService(ITransformerService transformer, IFirestoreService firestoreDb)
    {
        _transformer = transformer;
        _firestoreDb = firestoreDb;
    }

    public string GetUserFromAuth(HttpRequest request, string targetClaimType)
    {
        return _transformer.GetUserFromClaims(request, targetClaimType);
    }

    public async Task<ActionResult<List<TaskItemConfig>>> CreateNewItem(TaskItemConfig taskItem, string user)
    {
        List<string> users = new () { user };
        var firestoreTaskItem = _transformer.TransformToFireStoreConfig(taskItem, users);
        await _firestoreDb.AddAsync<FirestoreTaskItemConfig>(firestoreTaskItem);
        var newTaskItemList = await GetTaskItems(user);

        if (newTaskItemList.Value.FirstOrDefault(data => data.UniqueId == taskItem.UniqueId) == null)
            return new BadRequestObjectResult("Creation failed");

        return new OkObjectResult(newTaskItemList.Value);
    }

    public async Task<ActionResult<List<TaskItemConfig>>> GetTaskItems(string user)
    {
        var firestoreTaskItemList = await _firestoreDb.GetAllAsync<FirestoreTaskItemConfig>(user);
        var taskItemList = firestoreTaskItemList
            .Select(data => _transformer.TransformToTaskItemConfig(data))
            .ToList();
        taskItemList = taskItemList.OrderByDescending(data => data.DueDate).ToList();
        return taskItemList;
    }

    public async Task<List<string>> GetCurrentTaskOwner(TaskItemConfig taskItem)
    {
        var firestoreTaskItem = await _firestoreDb.GetAsync<FirestoreTaskItemConfig>(taskItem.UniqueId);
        var owners = firestoreTaskItem.Owner;
        return await Task.FromResult(owners);
    }

    public async Task<ActionResult<List<TaskItemConfig>>> GetTaskItemsByFilterSort(string user, 
        string? status = null, 
        string? name = null, 
        DateTime? dueDate = default, 
        string? order = null)
    {

        var firestoreTaskItemList = await _firestoreDb.GetAllByQueryAsync<FirestoreTaskItemConfig>(user, status, name);
        var taskItemList = firestoreTaskItemList
            .Select(data => _transformer.TransformToTaskItemConfig(data))
            .ToList();

        if (dueDate != null && order == null) 
        {
            return taskItemList;
        }

        if (dueDate != null)
        {
            taskItemList = taskItemList.Where(data => data.DueDate.Year == dueDate.Value.Year 
                                                      && data.DueDate.Month == dueDate.Value.Month
                                                      && data.DueDate.Day == dueDate.Value.Day).ToList();
        }  

        taskItemList = order.ToLower() == "ascending"
            ? taskItemList.OrderBy(data => data.DueDate).ToList()
            : taskItemList.OrderByDescending(data => data.DueDate).ToList();
        
        return taskItemList;
    }


    public async Task<ActionResult<List<TaskItemConfig>>> DeleteItem(TaskItemConfig taskItem, string user)
    {
        await _firestoreDb.DeleteAsync<FirestoreTaskItemConfig>(taskItem.UniqueId);
        var newTaskItemList = await GetTaskItems(user);
        if (newTaskItemList.Value.FirstOrDefault(data => data.UniqueId == taskItem.UniqueId) != null)
            return new BadRequestObjectResult("Deletion failed");

        return new OkObjectResult(newTaskItemList.Value);
    }


    public async Task<ActionResult<List<TaskItemConfig>>> UpdateItem(TaskItemConfig taskItem, string user, string? newItemUser)
    {
        var documentName = taskItem.UniqueId;
        var users = await GetCurrentTaskOwner(taskItem);
        if (newItemUser != null) users.Add(newItemUser);
        var firestoreTaskItem = _transformer.TransformToFireStoreConfig(taskItem, users);
        await _firestoreDb.UpdateAsync(firestoreTaskItem, documentName);
        var newTaskItemList = await GetTaskItems(user);
        if (newTaskItemList.Value.FirstOrDefault(data => data.UniqueId == taskItem.UniqueId) == null)
            return new BadRequestObjectResult("Update failed");
        return new OkObjectResult(newTaskItemList.Value);
    }



}