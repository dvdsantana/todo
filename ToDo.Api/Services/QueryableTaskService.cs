using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDo.API.Models;

namespace ToDo.API.Service
{
    public class QueryableTaskService : IQueryableTaskService
    {
        private readonly TaskContext _context;

        public QueryableTaskService(TaskContext context)
        {
            _context = context;
        }

        // This sintaxis is only for demo purpose. In a real scenario, this method 
        // will be more complex and we can't use the => operator like this.
        public async Task<IEnumerable<Models.Task>> GetAll() => 
            await _context.Tasks.ToListAsync();

        public async Task<Models.Task> GetById(string id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<IEnumerable<Models.Task>> GetCompleted()
        {
            return await _context.Tasks.Where(x => x.IsCompleted).ToListAsync();
        }

        public async Task<IEnumerable<Models.Task>> GetPending()
        {
            return await _context.Tasks.Where(x => !x.IsCompleted).ToListAsync();
        }
    }
}