using Microsoft.EntityFrameworkCore;
using ToDo.API.Models;
using ToDo.API.Service;
using Xunit;
using FluentAssertions;

namespace ToDo.Api.Tests.Services.CommandableTaskServices
{
    public class Delete_Should
    {
        private readonly TaskContext _context;

        private readonly ICommandableTaskService _service;
        private readonly IQueryableTaskService _queryService;
        
        public Delete_Should()
        {
            var options = new DbContextOptionsBuilder<TaskContext>()
                .UseInMemoryDatabase(databaseName: "Mock_database")
                .Options;

            _context = new TaskContext(options);
            _service = new CommandableTaskService(_context);
            _queryService = new QueryableTaskService(_context);
        }

        [Fact]
        public async System.Threading.Tasks.Task Delete_task()
        {
            // Arrange
            var task = new Task("fake description", true);
            var expected = await _service.Create(task);

            // Act
            var deleted_task = await _service.Delete(expected.Id);
            var isDeleted = await _queryService.GetById(expected.Id) == null;
            
            // Assert
            deleted_task.Id.Equals(expected.Id);
            isDeleted.Should().BeTrue();
        }
    }
}
