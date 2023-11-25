using Microsoft.AspNetCore.Mvc;
using TodoList.Service.Schema;

namespace TodoList.Service.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ToDoListController : ControllerBase
    {
    //    private static readonly string[] Summaries = new[]
    //    {
    //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    //};

        private readonly ILogger<ToDoListController> _logger;

        public ToDoListController(ILogger<ToDoListController> logger)
        {
            _logger = logger;
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
        public ActionResult GetTask()
        {
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