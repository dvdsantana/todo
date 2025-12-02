using Microsoft.EntityFrameworkCore;
using ToDo.API.Models;
using ToDo.API.Service;
using Xunit;
using FluentAssertions;

namespace ToDo.Api.Tests.Services.QuerybleTaskServices
{
    public class GetAll_Should
    {
        private readonly TaskContext _context;

        private readonly IQueryableTaskService _service;
        private readonly ICommandableTaskService _commandService;
        
        public GetAll_Should()
        {
            var options = new DbContextOptionsBuilder<TaskContext>()
                .UseInMemoryDatabase(databaseName: "Mock_database")
                .Options;

            _context = new TaskContext(options);
            _service = new QueryableTaskService(_context);
            _commandService = new CommandableTaskService(_context);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAll_empty_database()
        {
            // Arrange

            // Act
            var all_tasks = await _service.GetAll();

            // Assert
            all_tasks.Should().AllBeOfType(typeof(Task));
            all_tasks.Should().BeEmpty();
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAll_populated_database()
        {
            // Arrange
            var count_expected = 1;
            var task = new Task("fake description", true);
            var expected = _commandService.Create(task);

            // Act
            var all_tasks = await _service.GetAll();

            // Assert
            all_tasks.Should().AllBeOfType(typeof(Task));
            all_tasks.Should().HaveCount(count_expected);
        }
    }
}
