using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ToDo.API.Controllers
{
    interface ICommandableTask
    {
        Task<ActionResult<Models.Task>> Post(Models.Task task);

        Task<ActionResult<Models.Task>> Delete(string id);

        Task<ActionResult<Models.Task>> ToggleState(string id);
    }
}