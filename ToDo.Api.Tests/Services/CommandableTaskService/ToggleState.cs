using Microsoft.EntityFrameworkCore;
using ToDo.API.Models;
using ToDo.API.Service;
using Xunit;
using FluentAssertions;

namespace ToDo.Api.Tests.Services.CommandableTaskServices
{
    public class ToggleState_should
    {
        private readonly TaskContext _context;

        private readonly ICommandableTaskService _service;
        private readonly IQueryableTaskService _queryService;
        
        public ToggleState_should()
        {
            var options = new DbContextOptionsBuilder<TaskContext>()
                .UseInMemoryDatabase(databaseName: "Mock_database")
                .Options;

            _context = new TaskContext(options);
            _service = new CommandableTaskService(_context);
            _queryService = new QueryableTaskService(_context);
        }

        [Fact]
        public async System.Threading.Tasks.Task Change_state_to_completed()
        {
            // Arrange
            var task = new Task("fake pending task", false);
            var expected = await _service.Create(task);

            // Act
            var actual = await _service.ToggleState(expected.Id);
            var isCompleted = actual.IsCompleted == true;
            
            // Assert
            isCompleted.Should().BeTrue();
        }

        [Fact]
        public async System.Threading.Tasks.Task Change_state_to_pending()
        {
            // Arrange
            var task = new Task("fake completed task", true);
            var expected = await _service.Create(task);

            // Act
            var actual = await _service.ToggleState(expected.Id);
            var isCompleted = actual.IsCompleted == true;
            
            // Assert
            isCompleted.Should().BeFalse();
        }
    }
}
