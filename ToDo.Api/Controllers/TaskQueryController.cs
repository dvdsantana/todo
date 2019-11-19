using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDo.API.Service;
using Task = ToDo.API.Models.Task;

namespace ToDo.API.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskQueryController : ControllerBase, IQueryableTask
    {
        private readonly IQueryableTaskService _taskService;
        private readonly ILogger<TaskQueryController> _logger;

        public TaskQueryController(
            ILogger<TaskQueryController> logger
            , IQueryableTaskService taskService)
        {
            _logger = logger;
            _taskService = taskService;
        }

        /// <summary>
        /// Retrieve all tasks.
        /// </summary>
        /// <returns>List of all tasks saved.</returns>
        /// <response code="200">Returns the full tasks list.</response>
        /// <response code="400">If the tasks list is empty.</response> 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Task>>> GetAll()
        {
            var response = await _taskService.GetAll();

            return new ActionResult<IEnumerable<Models.Task>>(response);
        }

        /// <summary>
        /// Get only the completed tasks.
        /// </summary>
        /// <returns>A list of completed tasks.</returns>
        /// <response code="200">Returns the completed tasks.</response>
        /// <response code="400">If no one task is completed.</response> 
        [HttpGet("completed")]
        public async Task<ActionResult<IEnumerable<Task>>> GetCompleted()
        {
            var response = await _taskService.GetCompleted();

            if (response == null)
            {
                _logger.LogWarning("No completed tasks found.");
                return NotFound();
            }

            return new ActionResult<IEnumerable<Models.Task>>(response);
        }

        /// <summary>
        /// Get only the pending tasks.
        /// </summary>
        /// <returns>A list of pending tasks.</returns>
        /// <response code="200">Returns the pending tasks.</response>
        /// <response code="400">If no one task is pending.</response> 
        [HttpGet("pending")]
        public async Task<ActionResult<IEnumerable<Task>>> GetPending()
        {
            var response = await _taskService.GetPending();

            if (response == null)
            {
                _logger.LogWarning("No pending tasks found.");
                return NotFound();
            }

            return new ActionResult<IEnumerable<Models.Task>>(response);
        }

        /// <summary>
        /// Get a specific task.
        /// </summary>
        /// <returns>A task.</returns>
        /// <response code="200">Returns the task.</response>
        /// <response code="400">If the task not exist.</response> 
        [HttpGet("{id}")]
        public async Task<ActionResult<Task>> Get(string id)
        {
            var response = await _taskService.GetById(id);

            if (response == null)
            {
                _logger.LogWarning("Task ID({Id}) NOT FOUND", id);
                return NotFound();
            }

            return new ActionResult<Models.Task>(response);
        }
    }
}