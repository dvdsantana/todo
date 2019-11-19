using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDo.API.Service
{
    public interface IQueryableTaskService
    {
        Task<IEnumerable<Models.Task>> GetAll();
        
        Task<IEnumerable<Models.Task>> GetCompleted();

        Task<IEnumerable<Models.Task>> GetPending();

        Task<Models.Task> GetById(string id);
    }
}