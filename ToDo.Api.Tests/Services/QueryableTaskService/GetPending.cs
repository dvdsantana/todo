using Microsoft.EntityFrameworkCore;
using ToDo.API.Models;
using ToDo.API.Service;
using Xunit;
using FluentAssertions;

namespace ToDo.Api.Tests.Services.QuerybleTaskServices
{
    public class GetPending_Should
    {
        private readonly TaskContext _context;

        private readonly IQueryableTaskService _service;
        private readonly ICommandableTaskService _commandService;
        
        public GetPending_Should()
        {
            var options = new DbContextOptionsBuilder<TaskContext>()
                .UseInMemoryDatabase(databaseName: "Mock_database")
                .Options;

            _context = new TaskContext(options);
            _service = new QueryableTaskService(_context);
            _commandService = new CommandableTaskService(_context);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetPending_empty_database()
        {
            // Arrange

            // Act
            var pending_tasks = await _service.GetPending();

            // Assert
            pending_tasks.Should().AllBeOfType(typeof(Task));
            pending_tasks.Should().BeEmpty();
        }

        [Fact]
        public async System.Threading.Tasks.Task GetPending_populated_database()
        {
            // Arrange
            var count_expected = 1;
            var task = new Task("fake description");
            var expected = _commandService.Create(task);

            // Act
            var pending_tasks = await _service.GetPending();

            // Assert
            pending_tasks.Should().AllBeOfType(typeof(Task));
            pending_tasks.Should().HaveCount(count_expected);
        }
    }
}
