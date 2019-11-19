using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ToDo.API.Controllers
{
    interface IQueryableTask
    {
        Task<ActionResult<IEnumerable<Models.Task>>> GetAll();
        
        Task<ActionResult<IEnumerable<Models.Task>>> GetCompleted();
        
        Task<ActionResult<IEnumerable<Models.Task>>> GetPending();
        
        Task<ActionResult<Models.Task>> Get(string id);
    }
}