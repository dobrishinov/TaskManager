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
            //TaskManagementEnum choice = RenderMenu();

            while (true)
            {
                TaskManagementEnum choice = RenderMenu();

                try
                {
                    switch (choice)
                    {
                        case TaskManagementEnum.Select:
                            {
                                GetAll();
                                break;
                            }
                        case TaskManagementEnum.View:
                            {
                                View();
                                break;
                            }
                        case TaskManagementEnum.Insert:
                            {
                                Add();
                                break;
                            }
                        case TaskManagementEnum.Update:
                            {
                                Update();
                                break;
                            }
                        case TaskManagementEnum.Delete:
                            {
                                Delete();
                                break;
                            }
                        case TaskManagementEnum.Exit:
                            {
                                return;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                    Console.ReadKey(true);
                }
            }

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

        private void GetAll()
        {
            Console.Clear();

            TasksRepository tasksRepository = new TasksRepository("tasks.txt");
            List<TaskEntity> tasks = tasksRepository.GetAll(Auth.LoggedUser.Id);

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("#################|Get ALL|##################");
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

            Console.ReadKey(true);
        }

        private void View()
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("###############|View By ID|################");
            Console.ResetColor();

            Console.Write("Please enter task ID: ");
            int taskId = Convert.ToInt32(Console.ReadLine());

            TasksRepository tasksControllers = new TasksRepository("tasks.txt");

            TaskEntity task = tasksControllers.GetById(taskId);
            //
            if (task == null || task.ResponsibleUsers != Auth.LoggedUser.Id)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Task not found. Press Key To Return!");
                Console.ResetColor();
                Console.ReadKey(true);
                return;
            }

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

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Task found. Press Key To Return!");
            Console.ResetColor();

            Console.ReadKey(true);
        }

        private void Add()
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("###############|Add New Task|################");
            Console.ResetColor();

            TaskEntity task = new TaskEntity();

            Console.Write("Add Task Title: ");
            task.Title = Console.ReadLine();
            Console.Write("Add Task Content: ");
            task.Content = Console.ReadLine();
            Console.Write("Creator Name: ");
            task.Creator = Console.ReadLine();
            Console.Write("Enter ID to Responsible User: ");
            task.ResponsibleUsers = int.Parse(Console.ReadLine());
            Console.Write("Task Status(Working/Idle/Done): ");
            task.Status = Console.ReadLine();
            Console.Write("Working time in Hours: ");
            task.Time = Convert.ToInt32(Console.ReadLine());
            task.CreateTime = DateTime.Now;
            task.LastChange = DateTime.Now;
            task.CreatorId = Auth.LoggedUser.Id;

            TasksRepository usersControllers = new TasksRepository("tasks.txt");
            usersControllers.Save(task);

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Task saved successfully!");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        private void Update()
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("###############|Edit Task|################");
            Console.ResetColor();

            Console.Write("Please enter task ID: ");
            int taskId = Convert.ToInt32(Console.ReadLine());

            TasksRepository tasksRepository = new TasksRepository("tasks.txt");
            TaskEntity task = tasksRepository.GetById(taskId);

            if (task == null)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Task not found. Press Key To Return!");
                Console.ResetColor();
                Console.ReadKey(true);
                return;
            }

            Console.WriteLine("Editing task (" + task.Title + ") with ID: " + task.Id);

            Console.WriteLine("##########################################");

            Console.WriteLine("Old task title: " + task.Title);
            Console.Write("New Title:");
            string title = Console.ReadLine();

            Console.WriteLine("Old task content: " + task.Content);
            Console.Write("New Content: ");
            string content = Console.ReadLine();

            Console.WriteLine("##########################################");

            Console.WriteLine("Status: " + task.Status);
            Console.Write("New Status(Working/Idle/Done): ");
            string status = Console.ReadLine();

            Console.WriteLine("##########################################");

            Console.WriteLine("All working time in Hours: " + task.Time);
            Console.Write("Enter new time in Hours: ");
            int time = int.Parse(Console.ReadLine());

            task.LastChange = DateTime.Now;


            if (!string.IsNullOrEmpty(title))
                task.Title = title;
            if (!string.IsNullOrEmpty(content))
                task.Content = content;
            if (!string.IsNullOrEmpty(status))
                task.Status = status;
            //TODO maybe fix WorkingTime
            if (task.Time != time)
                task.Time = time;

            tasksRepository.Save(task);

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Task saved successfully. Press Key To Return!");
            Console.ResetColor();
            Console.ReadKey(true);
        }


        private void Delete()
        {
            TasksRepository tasksRepository = new TasksRepository("tasks.txt");

            Console.Clear();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("#########|Delete Task By ID|############");
            Console.ResetColor();

            Console.Write("Please enter task ID: ");
            int taskId = Convert.ToInt32(Console.ReadLine());

            TaskEntity task = tasksRepository.GetById(taskId);
            if (task == null || task.CreatorId != Auth.LoggedUser.Id)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Task not found. Press Key To Return!");
                Console.ResetColor();
            }
            else
            {
                tasksRepository.Delete(task);
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Task deleted successfully!");
                Console.ResetColor();
            }
            Console.ReadKey(true);
        }


    }
}
