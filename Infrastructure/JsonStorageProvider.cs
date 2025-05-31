using CryoTaskTracker.Domain.Interfaces;
using CryoTaskTracker.Domain.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;


namespace CryoTaskTracker.Infrastructure
{
    public class JsonStorageProvider : IStorageProvider
    {
        private const string FilePath = "tasks.json";

        public List<TaskModel> LoadTasks()
        {
            if (!File.Exists(FilePath))
                return new List<TaskModel>();
            var json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<List<TaskModel>>(json) ?? new List<TaskModel>();
        }

        public void SaveTasks(List<TaskModel> tasks)
        {
            var json = JsonConvert.SerializeObject(tasks, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }
    }
}