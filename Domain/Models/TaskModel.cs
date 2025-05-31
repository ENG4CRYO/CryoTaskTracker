using System;

namespace CryoTaskTracker.Domain.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public TaskState State { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; }
    }

    public enum TaskState
    {
        Todo,
        InProgress,
        Done
    }
}