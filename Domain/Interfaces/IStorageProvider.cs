using CryoTaskTracker.Domain.Models;
using System.Collections.Generic;

namespace CryoTaskTracker.Domain.Interfaces
{
    public interface IStorageProvider
    {
        List<TaskModel> LoadTasks();
        void SaveTasks(List<TaskModel> tasks);
    }
}