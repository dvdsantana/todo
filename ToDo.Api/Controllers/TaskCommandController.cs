using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDo.API.Service;
using Task = ToDo.API.Models.Task;

namespace ToDo.API.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskCommandController : ControllerBase, ICommandableTask
    {
        private readonly ILogger<TaskQueryController> _logger;
        private readonly ICommandableTaskService _taskService;

        public TaskCommandController(
            ILogger<TaskQueryController> logger
            , ICommandableTaskService taskService)
        {
            _logger = logger;
            _taskService = taskService;
        }

        /// <summary>
        /// Create a new task.
        /// </summary>
        /// <returns>The task created.</returns>
        /// <response code="200">Returns the newly created item.</response>
        /// <response code="400">If the item is null.</response> 
        [HttpPost]
        public async Task<ActionResult<Models.Task>> Post(Models.Task task)
        {
            var response = await _taskService.Create(task);

            return CreatedAtAction(nameof(Post), new { id = response.Id }, response);
        }

        /// <summary>
        /// Delete a task.
        /// </summary>
        /// <returns>The task deleted.</returns>
        /// <response code="200">Returns the deleted item.</response>
        /// <response code="400">If the task not exist.</response> 
        [HttpDelete("{id}")]
        public async Task<ActionResult<Models.Task>> Delete(string id)
        {
            var response = await _taskService.Delete(id);
            
            if (response == null)
            {
                _logger.LogWarning("Task ID({Id}) NOT FOUND", id);
                return NotFound();
            }

            return new ActionResult<Models.Task>(response);
        }

        /// <summary>
        /// Change the task state.
        /// </summary>
        /// <returns>The task updated.</returns>
        /// <response code="200">Returns the task with state updated.</response>
        /// <response code="400">If the task not exist.</response> 
        [HttpPut("{id}")]
        public async Task<ActionResult<Task>> ToggleState(string id)
        {
            var response = await _taskService.ToggleState(id);
            
            if (response == null)
            {
                _logger.LogWarning("Task ID({Id}) NOT FOUND", id);
                return NotFound();
            }

            return new ActionResult<Models.Task>(response);
        }

    }
}