using Microsoft.Extensions.DependencyInjection;
using CryoTaskTracker.Application;
using CryoTaskTracker.Infrastructure;
using CryoTaskTracker.Domain.Interfaces;
using CryoTaskTracker.Domain.Models;
using System.Threading.Tasks;
using CryoTaskTracker.Utilities;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;


class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddSingleton<ITaskService, TaskService>();
        services.AddSingleton<IStorageProvider, JsonStorageProvider>();
        var provider = services.BuildServiceProvider();

        var taskService = provider.GetRequiredService<ITaskService>();

        Utility.PrintWelcomeMessage();
        while (true)
        {

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("\nEnter Command: ");
            Console.ResetColor();

            string input = Console.ReadLine()?.Trim().ToLower();
            List<string> command = Utility.ParseCommand(input);
            if (command.Count >= 1)
            {
                switch (command[0])
                {
                    case "add":
                        if (command.Count >= 1)
                        {
                            string desc = command[1];
                            taskService.AddTask(desc);
                        }
                        else
                        {
                            Utility.PrintInValidCommand();
                        }
                        break;

                    case "list":

                        if (command.Count > 1)
                        {
                            if (command[1] == "todo" | command[1] == "done" | command[1] == "in-progress")
                            {
                                listTaskByStatus(command[1].ToLower());
                                break;
                            }
                            else
                            {
                                Utility.PrintInValidCommand();
                            }
                        }
                        else if (!string.IsNullOrEmpty(command[0]))
                        {
                            listAllTask();
                        }
                        else
                        {
                            Utility.PrintInValidCommand();

                        }

                        break;

                    case "update":
                        if (command.Count >= 2)
                        {
                            if (int.TryParse(command[1], out int updateId))
                            {
                                var newDesc = command[2];
                                taskService.UpdateTaskDescription(updateId, newDesc);
                            }
                            else
                            {
                                Utility.PrintInValidCommand();
                            }
                        }
                        else { Utility.PrintInValidCommand(); }
                            break;

                    case "mark":
                        if (command.Count >= 3)
                        {
                            if (int.TryParse(command[1], out int idMarkTask))
                            {
                                taskService.MarkTask(idMarkTask, command[2].ToLower());
                            }
                            else
                            {
                                Utility.PrintInValidCommand();
                            }
                        }

                        break;


                    case "delete":
                        if (command.Count >= 2)
                        {

                            if (int.TryParse(command[1], out int deleteId))
                                taskService.DeleteTask(deleteId);
                            else
                                Utility.PrintInValidCommand();
                        }
                        else
                        {
                            Utility.PrintInValidCommand();
                        }
                            break;

                    case "help":
                        if (command.Count >= 1 & command.Count < 2)
                        Utility.PrintHelpCommands();
                        else
                            Utility.PrintInValidCommand();
                        
                            break;

                    case "exit":
                        if (command.Count >= 1 & command.Count < 2)
                            return;
                        else 
                             Utility.PrintInValidCommand();
                        break;


                    case "clear":
                        if (command.Count >= 1 & command.Count < 2)
                        {
                            Console.Clear();
                            Utility.PrintWelcomeMessage();
                        }
                        else { Utility.PrintInValidCommand(); }
                                break;

                            default:
                                Utility.PrintInValidCommand();
                                break;
                            }
            }
            else
            {
                Utility.PrintInValidCommand();
            }


        }

        string Capitalize(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            return char.ToUpper(input[0]) + input.Substring(1);
        }


        void listAllTask()
        {
            var tasks = taskService.GetAllTasks();

            Utility.PrintHeaderTask();

            string NORMAL = Console.IsOutputRedirected ? "" : "\x1b[39m";
            string RED = Console.IsOutputRedirected ? "" : "\x1b[91m";
            string GREEN = Console.IsOutputRedirected ? "" : "\x1b[92m";
            string YELLOW = Console.IsOutputRedirected ? "" : "\x1b[93m";

            if (tasks != null & tasks.Count > 0)
            {

                foreach (var task in tasks)
                {
                    string stateColour = "";
                    if (task.State == TaskState.Done)
                    {
                        stateColour = GREEN;
                    }
                    else if (task.State == TaskState.InProgress)
                    {
                        stateColour = YELLOW;
                    }
                    else
                    {
                        stateColour = RED;
                    }
                    Console.WriteLine($"{task.Id.ToString().PadRight(5)}" +
                    $"{task.Description.PadRight(35)}" +
                        $"{stateColour}{task.State.ToString().PadRight(18)}{NORMAL}" +
                        $"{task.CreatedAt.ToString("M/d/yyyy hh:mm:ss tt").PadRight(25)}" +
                        $"{task.UpdatedAt.ToString("M/d/yyyy hh:mm:ss tt")}");

                }
            }
            else
            {
                Console.WriteLine("There Are No Tasks !");
            }
        }


        void listTaskByStatus(string State)
        {

            var tasks = taskService.GetAllTasks();

            string NORMAL = Console.IsOutputRedirected ? "" : "\x1b[39m";
            string RED = Console.IsOutputRedirected ? "" : "\x1b[91m";
            string GREEN = Console.IsOutputRedirected ? "" : "\x1b[92m";
            string YELLOW = Console.IsOutputRedirected ? "" : "\x1b[93m";
            int count = 0;

            if (State == "in-progress" | State == "done" | State == "todo")
            {
                Utility.PrintHeaderTask();
                if (tasks != null & tasks.Count > 0)
                {

                    foreach (var task in tasks)
                    {
                        string stateColour = "";
                        if (task.State == TaskState.Done)
                        {
                            stateColour = GREEN;
                        }
                        else if (task.State == TaskState.InProgress)
                        {
                            stateColour = YELLOW;
                        }
                        else
                        {
                            stateColour = RED;
                        }

                        if (task.State == Utility.stringToEnum(State))
                        {
                            Console.WriteLine($"{task.Id.ToString().PadRight(5)}" +
                            $"{task.Description.PadRight(35)}" +
                                $"{stateColour}{task.State.ToString().PadRight(18)}{NORMAL}" +
                                $"{task.CreatedAt.ToString("M/d/yyyy hh:mm:ss tt").PadRight(25)}" +
                                $"{task.UpdatedAt.ToString("M/d/yyyy hh:mm:ss tt")}");
                            count++;
                        }
                    }

                }
                if (count == 0)
                {
                    Console.WriteLine($"There Are No Tasks With State : {State}!");
                }


            }
            else
            {
                Utility.PrintInValidCommand();
                return;
            }
        }

    }
}
