using System.Threading.Tasks;

namespace ToDo.API.Service
{
    public interface ICommandableTaskService
    {
        Task<Models.Task> Create(Models.Task task);

        Task<Models.Task> ToggleState(string id);

        Task<Models.Task> Delete(string id);

    }
}