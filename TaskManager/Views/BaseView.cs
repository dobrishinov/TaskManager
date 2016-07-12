namespace TaskManager.Views
{
    using TaskManager.Services;
    using TaskManager.Repositories;
    using TaskManager.Entites;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class BaseView
    {
        public virtual void Show()
        {
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
                        case TaskManagementEnum.CommentAdd:
                            {
                                CommentAdd();
                                break;
                            }
                        case TaskManagementEnum.CommentEdit:
                            {
                                CommentEdit();
                                break;
                            }
                        case TaskManagementEnum.CommentDelete:
                            {
                                CommentDelete();
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

        private TaskManagementEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();

                TasksRepository tasksRepository = new TasksRepository("tasks.txt");
                List<TaskEntity> tasks = tasksRepository.GetAll(Auth.LoggedUser.Id);

                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("###############|Tasks manager|##############");
                Console.ResetColor();

                foreach (var task in tasks)
                {
                    Console.WriteLine("Task ID: " + task.Id + " | Title: " + task.Title + " | Create by: " + task.Creator + " | Response by: " + task.ResponsibleUsers);
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("################################################################################################");
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
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("#############|Comments Options|#############");
                Console.ResetColor();
                Console.WriteLine("[1] - Add comment to Task");
                Console.WriteLine("[2] - Edit comment");
                Console.WriteLine("[3] - Delete comment");
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
                    case "1":
                        {
                            return TaskManagementEnum.CommentAdd;
                        }
                    case "2":
                        {
                            return TaskManagementEnum.CommentEdit;
                        }
                    case "3":
                        {
                            return TaskManagementEnum.CommentDelete;
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

        public virtual void GetAll()
        {

        }

        public virtual void View()
        {

        }

        public virtual void CommentAdd()
        {

        }

        public virtual void CommentEdit()
        {

        }

        public virtual void CommentDelete()
        {

        }

        public virtual void Add()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Delete()
        {

        }
    }
}
