using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ToDo.API.Models;

namespace ToDo.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Seed the in memory database only for develop.
            // Remove this block; it's not required in the project.
            //SeedInMemoryDatabse();
            
            CreateHostBuilder(args).Build().Run();
        }

        private static void SeedInMemoryDatabse()
        {
            var options = new DbContextOptionsBuilder<TaskContext>()
                .UseInMemoryDatabase(databaseName: "TasksList")
                .Options;
            using (var context = new TaskContext(options))
            {
                context.Database.EnsureCreated();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
