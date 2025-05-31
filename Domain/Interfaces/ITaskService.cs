using CryoTaskTracker.Domain.Models;
using System.Collections.Generic;

namespace CryoTaskTracker.Domain.Interfaces
{
    public interface ITaskService
    {
        void AddTask(string description);
        List<TaskModel> GetAllTasks();
        void UpdateTaskDescription(int id, string newDescription);
        void MarkTask(int id,string State);
        List<TaskModel> GetTasksByState(TaskState state);
        void DeleteTask(int id);
    }
}
