using Microsoft.EntityFrameworkCore;
using ToDo.API.Models;
using ToDo.API.Service;
using Xunit;
using FluentAssertions;

namespace ToDo.Api.Tests.Services.CommandableTaskServices
{
    public class Create_Should
    {
        private readonly TaskContext _context;

        private readonly ICommandableTaskService _service;
        
        public Create_Should()
        {
            var options = new DbContextOptionsBuilder<TaskContext>()
                .UseInMemoryDatabase(databaseName: "Mock_database")
                .Options;

            _context = new TaskContext(options);
            _service = new CommandableTaskService(_context);
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_task()
        {
            // Arrange
            var expected = new Task("fake description", true);

            // Act
            var actual = await _service.Create(expected);

            // Assert

            // actual.Should().NotBeNull();
            // actual.Description.Should().Be("fake description");
            actual.Should().BeEquivalentTo(expected, x => x.Excluding(y => y.Id));
            actual.Id.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_task_as_pending()
        {
            // Arrange
            var expected = new Task("newly task as pending", false);
            var no_state = new Task("newly task as pending");
            
            // Act
            var actual = await _service.Create(expected);
            var actual_no_state = await _service.Create(no_state);

            // Assert
            actual.IsCompleted.Should().Be(false);
            actual_no_state.IsCompleted.Should().Be(false);
        }
    }
}
