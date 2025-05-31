using CryoTaskTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CryoTaskTracker.Utilities
{
    internal class Utility
    {
        public static void PrintWelcomeMessage()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("Welcome !");
            Console.WriteLine("Type \"Help\" To See The Commands");

            Console.ResetColor();
        }
        public static void PrintHeaderTask()
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("ID".PadRight(5) +
                  "Description".PadRight(35) +
                  "State".PadRight(18) +
                  "Created At".PadRight(25) +
                  "Updated At");
            Console.ResetColor();
        }
        public static List<string> ParseCommand(string input)
        {
            List<string> list = new List<string>();

            string pattern = @"^(\w+)(?:\s+(\d+))?(?:\s+""(.*?)"")?(?:\s+(\w[\w\-]*))?$";
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                for (int i = 1; i < match.Groups.Count; i++)
                {
                    if (match.Groups[i].Success && !string.IsNullOrWhiteSpace(match.Groups[i].Value))
                    {
                        list.Add(match.Groups[i].Value);
                    }
                }
            }

            return list;
        }
        public static void PrintHelpCommands()
        {
            var helpCommands = GetAllHelpCommands();
            int count = 1;
            if (helpCommands != null)
            {
                foreach (var item in helpCommands)
                {
                    PrintHelpMessage(count + ". " + item);
                    count++;
                }
            }
        }
        private static void PrintHelpMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n" + message);
            Console.ResetColor();
        }

        private static List<string> GetAllHelpCommands()
        {
            return new List<string>
            {
                "add \"Task Description\" -> To add a new task, type add with task description",
                "update Task Id \"Task Description\" -> To update a task, type update with task id and task description",
                "delete Task Id -> To delete a task, type delete with task id",
                "mark in-progress Task Id -> To mark a task to in progress, type mark-in-progress with task id",
                "mark done Task Id -> To mark a task to done, type mark-done with task id",
                "list -> To list all task with its current status",
                "list done -> To list all task with done status",
                "list todo  -> To list all task with todo status",
                "list in progress  -> To list all task with in-progress status",
                "exit -> To exit from app",
                "clear - To clear console window"
            };
        }
        public static void PrintInValidCommand()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\ninValid Command!, Try Again");
            Console.ResetColor();
        }
        public static string enumToString(TaskState Status)
        {
            if (Status == TaskState.Todo)
            {
                return "Todo";
            }
            if (Status == TaskState.InProgress)
            {
                return "InProgress";
            }
            if (Status == TaskState.Done)
            {
                return "Done";
            }
            return string.Empty;
        }

        public static TaskState stringToEnum(string status)
        {
            if (status == "done") return TaskState.Done;
            if (status == "in-progress") return TaskState.InProgress;
            if (status == "todo") return TaskState.Todo;

            return TaskState.Todo;

        }
    }
}
