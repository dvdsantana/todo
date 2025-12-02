using Microsoft.EntityFrameworkCore;
using ToDo.API.Models;
using ToDo.API.Service;
using Xunit;
using FluentAssertions;

namespace ToDo.Api.Tests.Services.QuerybleTaskServices
{
    public class GetById_Should
    {
        private readonly TaskContext _context;

        private readonly IQueryableTaskService _service;
        private readonly ICommandableTaskService _commandService;
        
        public GetById_Should()
        {
            var options = new DbContextOptionsBuilder<TaskContext>()
                .UseInMemoryDatabase(databaseName: "Mock_database")
                .Options;

            _context = new TaskContext(options);
            _service = new QueryableTaskService(_context);
            _commandService = new CommandableTaskService(_context);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetById_empty_database()
        {
            // Arrange
            var task = new Task("fake description");

            // Act
            var actual = await _service.GetById(task.Id);

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        public async System.Threading.Tasks.Task GetById_populated_database()
        {
            // Arrange
            var task = new Task("fake description");
            var expected = await _commandService.Create(task);

            // Act
            var actual = await _service.GetById(expected.Id);

            // Assert
            actual.Should().BeEquivalentTo(expected);
            actual.Id.Should().NotBeNullOrEmpty();
        }
    }
}
