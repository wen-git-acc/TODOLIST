using Google.Cloud.Firestore.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Service.Extensions;
using TodoList.Service.Schema;
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

        public ToDoListController(ILogger<ToDoListController> logger, IJWTManagerRepository jwtManagerRepository, IFirestoreService firestoreDb)
        {
            _logger = logger;
            _jwtManagerRepository = jwtManagerRepository;
            _firestoreDb = firestoreDb;
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
        public async Task<ActionResult> CreateNewTask([FromBody] TaskItemConfig taskItem)
        {
            var token = Request.Headers.Authorization;
            var ownerEmail = _jwtManagerRepository.ReadClaims(token, "Email");
            var firebaseData = new FirestoreTaskItemConfig
            {
                DocumentId = taskItem.UniqueId,
                DueDate = taskItem.DueDate,
                Name = taskItem.Name,
                Owner = new List<string>{ownerEmail},
                Status = taskItem.Status.ToString(),
                UniqueId = taskItem.UniqueId,
                Description = taskItem.Description,
            };

            await _firestoreDb.AddAsync<FirestoreTaskItemConfig>(firebaseData);
            return Ok();
        }

        [HttpGet]
        [ActionName("GetTask")]
        public async Task<ActionResult<List<TaskItemConfig>>> GetTasks()
        {
            var token = Request.Headers.Authorization;
            var ownerEmail=_jwtManagerRepository.ReadClaims(token, "Email");
            var response = await _firestoreDb.GetAllAsync<FirestoreTaskItemConfig>(ownerEmail);
          
            var taskItemList = response.Select(data => new TaskItemConfig
            {
                UniqueId = data.UniqueId,
                Description = data.Description,
                DueDate = data.DueDate,
                Name = data.Name,
                Status = Enum.TryParse(data.Status.ToLower(), out TaskStatus value) ? value : null,
            }).ToList();
            
            return Ok(taskItemList);
        }

        [HttpGet]
        [ActionName("GetFilterTask")]
        public ActionResult GetFilterTaskTask(string? status = null, DateTime dueDate = default)
        {
            Console.WriteLine(status);
            Console.WriteLine(dueDate.Date);
            Console.WriteLine(DateTime.MinValue);
            return Ok();
        }
        
        
        [HttpGet]
        [ActionName("GetSortTask")]
        public ActionResult GetSortTask(string? status = null,  string? name = null, DateTime? dueDate = default)
        {
            //If all null need to return bad reqeust;
            return Ok();
        }

        [HttpDelete]
        [ActionName("DeleteTask")]
        public async Task<ActionResult<List<TaskItemConfig>>> DeleteTask([FromBody] TaskItemConfig taskItem)
        {
            var token = Request.Headers.Authorization;
            var ownerEmail = _jwtManagerRepository.ReadClaims(token, "Email");
            await _firestoreDb.DeleteAsync<FirestoreTaskItemConfig>(taskItem.UniqueId);
            var response = await _firestoreDb.GetAllAsync<FirestoreTaskItemConfig>(ownerEmail);
            var taskItemList = response.Select(data => new TaskItemConfig
            {
                UniqueId = data.UniqueId,
                Description = data.Description,
                DueDate = data.DueDate,
                Name = data.Name,
                Status = Enum.TryParse(data.Status.ToLower(), out TaskStatus value) ? value : null,
            }).ToList();

            return Ok(taskItemList);
        }

        [HttpDelete]
        [ActionName("UpdateTask")]
        public async void UpdateTask([FromBody] TaskItemConfig taskItem)
        {
            //var token = Request.Headers.Authorization;
            //var ownerEmail = _jwtManagerRepository.ReadClaims(token, "Email");
            //var currentFirebaseData = _firestoreDb.GetAsync<>()
            //var firebaseData = new FirestoreTaskItemConfig
            //{
            //    DocumentId = taskItem.UniqueId,
            //    DueDate = taskItem.DueDate,
            //    Name = taskItem.Name,
            //    Owner = new List<string> { ownerEmail},
            //    Status = "InProgress",
            //    UniqueId = id,
            //    Description = "this is testing 2"
            //};
            //_firestoreDb.UpdateAsync(taskItem, taskItem.UniqueId);
            //var taskStatus = taskItem.Status;
            //Console.WriteLine(taskStatus);
        }




    }
}