using CryoTaskTracker.Domain.Interfaces;
using CryoTaskTracker.Domain.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using CryoTaskTracker.Utilities;

namespace CryoTaskTracker.Application
{
    public class TaskService : ITaskService
    {
        private readonly IStorageProvider _storage;
        private List<TaskModel> _tasks;

        public TaskService(IStorageProvider storage)
        {
            _storage = storage;
            _tasks = _storage.LoadTasks();
        }

        public void AddTask(string description)
        {
            var newTask = new TaskModel
            {
                Id = _tasks.Count > 0 ? _tasks.Max(t => t.Id) + 1 : 1,
                Description = description,
                State = TaskState.Todo
            };
            _tasks.Add(newTask);
            _storage.SaveTasks(_tasks);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Task added successfully.");
            Console.ResetColor();
        }

        public List<TaskModel> GetAllTasks() => _tasks;

        public void UpdateTaskDescription(int id, string newDescription)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Task not found.");
                Console.ResetColor();

                return;
            }
            task.Description = newDescription;
            task.UpdatedAt = DateTime.Now;
            _storage.SaveTasks(_tasks);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Task description updated.");
            Console.ResetColor();
        }

        public void MarkTask(int id, string State)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Task not found.");
                Console.ResetColor();
                return;
            }
            TaskState newState = Utility.stringToEnum(State);
            if (Enum.TryParse<TaskState>(Utility.enumToString(newState),out var state))
            {
                task.State = (TaskState)state;
                _storage.SaveTasks(_tasks);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Task state updated.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid state.");
                Console.ResetColor();
            }
        }

        public List<TaskModel> GetTasksByState(TaskState state)
        {
            return _tasks.Where(t => t.State == state).ToList();
        }

        public void DeleteTask(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                _tasks.Remove(task);
                _storage.SaveTasks(_tasks);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Task deleted.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Task not found.");
                Console.ResetColor();
            }
        }
    }
}