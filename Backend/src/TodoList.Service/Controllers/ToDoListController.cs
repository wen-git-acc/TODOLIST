using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Service.Extensions;
using TodoList.Service.Schema;
using TodoList.Service.Services.JWTRepository;

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
        public ActionResult CreateNewTask([FromBody] TaskItemConfig taskItem)
        {
            var taskStatus = taskItem.Status;
            Console.WriteLine(taskStatus);
            return Ok();
        }

        [HttpGet]
        [ActionName("GetTask")]
        public async Task<ActionResult> GetTask()
        {
            var token = Request.Headers.Authorization;
            var email=_jwtManagerRepository.ReadClaims(token, "Email");
            //_firestoreDb.AddData("TaskItemCollections");
            var document = "nir3bnP82aCpHVUaIL4m";
            await _firestoreDb.GetAllAsync<FirestoreTaskItemConfig>("TaskItemCollections");
            //_firestoreDb.GetData<FirestoreTaskItemConfig>("TaskItemCollections",document);

            Console.WriteLine(email);
            return Ok();
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
        public ActionResult DeleteTask([FromBody] TaskItemConfig taskItem)
        {
            var taskStatus = taskItem.Status;
            Console.WriteLine(taskStatus);
            return Ok();
        }

        [HttpDelete]
        [ActionName("UpdateTask")]
        public ActionResult UpdateTask([FromBody] TaskItemConfig taskItem)
        {
            var taskStatus = taskItem.Status;
            Console.WriteLine(taskStatus);
            return Ok();
        }




    }
}