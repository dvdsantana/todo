using Microsoft.EntityFrameworkCore;
using ToDo.API.Models;
using ToDo.API.Service;
using Xunit;
using FluentAssertions;

namespace ToDo.Api.Tests.Services.QuerybleTaskServices
{
    public class GetCompleted_Should
    {
        private readonly TaskContext _context;

        private readonly IQueryableTaskService _service;
        private readonly ICommandableTaskService _commandService;
        
        public GetCompleted_Should()
        {
            var options = new DbContextOptionsBuilder<TaskContext>()
                .UseInMemoryDatabase(databaseName: "Mock_database")
                .Options;

            _context = new TaskContext(options);
            _service = new QueryableTaskService(_context);
            _commandService = new CommandableTaskService(_context);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetCompleted_empty_database()
        {
            // Arrange

            // Act
            var completed_tasks = await _service.GetCompleted();

            // Assert
            completed_tasks.Should().AllBeOfType(typeof(Task));
            completed_tasks.Should().BeEmpty();
        }

        [Fact]
        public async System.Threading.Tasks.Task GetCompleted_populated_database()
        {
            // Arrange
            var count_expected = 1;
            var task = new Task("fake description", true);
            var expected = _commandService.Create(task);

            // Act
            var completed_tasks = await _service.GetCompleted();

            // Assert
            completed_tasks.Should().AllBeOfType(typeof(Task));
            completed_tasks.Should().HaveCount(count_expected);
        }
    }
}
