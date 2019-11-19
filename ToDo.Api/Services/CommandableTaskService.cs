using System.Linq;
using System.Threading.Tasks;
using ToDo.API.Models;

namespace ToDo.API.Service
{
    public class CommandableTaskService : ICommandableTaskService
    {
        private readonly TaskContext _context;

        public CommandableTaskService(TaskContext context)
        {
            _context = context;
        }
        public async Task<Models.Task> Create(Models.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<Models.Task> Delete(string id)
        {
            var task = FindTaskById(id);
            if (task == null)
            {
                return null;
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<Models.Task> ToggleState(string id)
        {
            var task = FindTaskById(id);
            if (task == null)
            {
                return null;
            }

            // Ask to object to execute action instead of set property.
            task.ToggleState();

            await _context.SaveChangesAsync();

            return task;
        }

        private Models.Task FindTaskById(string id) => _context.Tasks.Find(id);
    }
}