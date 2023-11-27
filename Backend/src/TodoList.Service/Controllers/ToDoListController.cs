using Google.Cloud.Firestore.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Service.Extensions;
using TodoList.Service.Schema;
using TodoList.Service.Services.Database;
using TodoList.Service.Services.JWTRepository;
using static Google.Rpc.Context.AttributeContext.Types;
using TaskStatus = TodoList.Service.Schema.TaskStatus;

namespace TodoList.Service.Controllers
{
    [ApiController]
    [Authorize(Roles = "user")]
    [Route("[controller]/[action]")]
    public class ToDoListController : ControllerBase
    {
    //    private static readonly string[] Summaries = new[]
    //    {
    //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    //};

        private readonly ILogger<ToDoListController> _logger;
        private readonly IJWTManagerRepository _jwtManagerRepository;
        private readonly IFirestoreService _firestoreDb;
        private readonly IDatabaseService _database;

        public ToDoListController(ILogger<ToDoListController> logger, IJWTManagerRepository jwtManagerRepository, IFirestoreService firestoreDb, IDatabaseService database)
        {
            _logger = logger;
            _jwtManagerRepository = jwtManagerRepository;
            _firestoreDb = firestoreDb;
            _database = database;
        }

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        [HttpPost]
        [ActionName("CreateTask")]
        public async Task<ActionResult<List<TaskItemConfig>>> CreateNewTask([FromBody] TaskItemConfig taskItem)
        {

            var user = _database.GetUserFromAuth(Request, "Email");
            var newTaskItemList = await _database.CreateNewItem(taskItem, user);
            return newTaskItemList;
        }

        [HttpGet]
        [ActionName("GetTask")]
        public async Task<ActionResult<List<TaskItemConfig>>> GetTasks()
        {
            var user = _database.GetUserFromAuth(Request, "Email");
            var taskItemList = await _database.GetTaskItems(user);
            return taskItemList;
        }

        [HttpGet]
        [ActionName("GetTaskByFilterOrSort")]
        public async Task<ActionResult<List<TaskItemConfig>>> GetTasksByFilterOrSort(string? status = null, string? name = null, DateTime? dueDate = default, string? order = "descending")
        {
            //Assume order has 2 options: ascending, descending;
            var user = _database.GetUserFromAuth(Request, "Email");
            var taskItemList = await _database.GetTaskItemsByFilterSort(user,
                status,
                name,
                dueDate,
                order);
            return taskItemList;
        }


        [HttpDelete]
        [ActionName("DeleteTask")]
        public async Task<ActionResult<List<TaskItemConfig>>> DeleteTask([FromBody] TaskItemConfig taskItem)
        {
            var user = _database.GetUserFromAuth(Request, "Email");
            var newTaskItemList = await _database.DeleteItem(taskItem, user);
            return newTaskItemList;
        }

        [HttpPut]
        [ActionName("UpdateTask")]
        public async Task<ActionResult<List<TaskItemConfig>>> UpdateTask([FromBody] TaskItemConfig taskItem, string? newItemUser = null)
        {
            var user = _database.GetUserFromAuth(Request, "Email");
            var newTaskItemList = await _database.UpdateItem(taskItem, user, newItemUser);
            return newTaskItemList;
        }




    }
}