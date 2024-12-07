using HashStrike.Api.Models.Data;
using HashStrike.Api.Services;
using HashStrike.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace HashStrike.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly AnswerService _answerService;
        public TasksController(ApplicationContext db)
        {
            _db = db;
            _answerService = new AnswerService(db);
        }
        [HttpPost("create")]
        public IActionResult CreateTask([FromBody] TaskModel taskModel)
        {
            if (taskModel != null)
            {
                Models.Task newTask = new Models.Task(taskModel.HashType, taskModel.Hash, taskModel.MinLineLength, taskModel.MaxLineLength,
                    taskModel.HasCapitalLetters, taskModel.HasSmallLetters, taskModel.HasNumbers, taskModel.HasSpecialCharacters);
                _db.Tasks.Add(newTask);
                _db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("answers")]
        public IActionResult GetAnswers()
        {
            var answers = _answerService.GetAnswers();
            _answerService.CleanAnswers();
            return Ok(answers);
        }

        [HttpGet("test")]
        public IActionResult TestApi()
        {
            return Ok("Server is working");
        }
    }
}
