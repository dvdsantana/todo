using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToDo.API.Models
{
    public class Task
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 3)]
        public string Description { get; set; }
        
        [DefaultValue(false)]
        public bool IsCompleted { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public Task()
        {
            Id = Guid.NewGuid().ToString("N");
        }
        
        public Task(string description, bool isCompleted = false) : this()
        {
            Description = description;
            IsCompleted = isCompleted;
            CreatedAt = DateTime.Now;
        }

        public Task ToggleState()
        {
            IsCompleted = !this.IsCompleted;

            return this;
        }

    }
}