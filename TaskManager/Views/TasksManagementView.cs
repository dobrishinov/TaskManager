namespace TaskManager.Views
{
    using TaskManager.Entites;
    using TaskManager.Repositories;
    using TaskManager.Services;
    using System;
    using System.Collections.Generic;

    public class TasksManagementView : TaskEntity
    {
        public void Show()
        {
            TaskManagementEnum choice = RenderMenu();

        }

        protected TaskManagementEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();

                TasksRepository tasksController = new TasksRepository("tasks.txt");
                List<TaskEntity> tasks = tasksController.GetAll(Auth.LoggedUser.Id);

                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("###############|Tasks manager|##############");
                Console.ResetColor();

                foreach (var task in tasks)
                {
                    Console.WriteLine("Task ID: " + task.Id);
                    Console.WriteLine("Task Title: " + task.Title);
                    Console.WriteLine("Tasks Content: " + task.Content);
                    Console.WriteLine("Task create by: " + task.Creator);
                    Console.WriteLine("Creation Date: " + task.CreateTime);
                    Console.WriteLine("Last Change Date: " + task.LastChange);
                    Console.WriteLine("############################################");
                    Console.WriteLine("Task status: " + task.Status);
                    Console.WriteLine("Task working time: " + task.Time);
                    Console.WriteLine("Task response by: " + task.ResponsibleUsers);

                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("############################################");
                    Console.ResetColor();
                }

                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("#############|Available Options|############");
                Console.ResetColor();

                Console.WriteLine("[G]et all Tasks");
                Console.WriteLine("[V]iew Tasks");
                Console.WriteLine("[A]dd Tasks");
                Console.WriteLine("[E]dit Tasks");
                Console.WriteLine("[D]elete Tasks");
                Console.WriteLine("E[x]it");

                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "G":
                        {
                            return TaskManagementEnum.Select;
                        }
                    case "V":
                        {
                            return TaskManagementEnum.View;
                        }
                    case "A":
                        {
                            return TaskManagementEnum.Insert;
                        }
                    case "E":
                        {
                            return TaskManagementEnum.Update;
                        }
                    case "D":
                        {
                            return TaskManagementEnum.Delete;
                        }
                    case "X":
                        {
                            return TaskManagementEnum.Exit;
                        }
                    default:
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid Choice");
                            Console.ReadKey(true);
                            Console.ResetColor();

                            break;
                        }
                }
            }
        }

    }
}
