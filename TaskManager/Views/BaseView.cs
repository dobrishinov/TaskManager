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
        public virtual void ShowTaskManagement()
        {
            while (true)
            {
                TaskManagementEnum choice = TaskRenderMenu();

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

        private TaskManagementEnum TaskRenderMenu()
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

        public virtual void ShowUserManagement()
        {
            while (true)
            {
                UserManagementEnum choice = UserRenderMenu();

                try
                {
                    switch (choice)
                    {
                        case UserManagementEnum.Select:
                            {
                                GetAll();
                                break;
                            }
                        case UserManagementEnum.View:
                            {
                                View();
                                break;
                            }
                        case UserManagementEnum.Insert:
                            {
                                Add();
                                break;
                            }
                        case UserManagementEnum.Update:
                            {
                                Update();
                                break;
                            }
                        case UserManagementEnum.Delete:
                            {
                                Delete();
                                break;
                            }
                        case UserManagementEnum.Exit:
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

        private UserManagementEnum UserRenderMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("#############|User management|##############");
                Console.ResetColor();

                Console.WriteLine("[G]et all Users");
                Console.WriteLine("[V]iew User");
                Console.WriteLine("[A]dd User");
                Console.WriteLine("[E]dit User");
                Console.WriteLine("[D]elete User");
                Console.WriteLine("E[x]it");

                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "G":
                        {
                            return UserManagementEnum.Select;
                        }
                    case "V":
                        {
                            return UserManagementEnum.View;
                        }
                    case "A":
                        {
                            return UserManagementEnum.Insert;
                        }
                    case "E":
                        {
                            return UserManagementEnum.Update;
                        }
                    case "D":
                        {
                            return UserManagementEnum.Delete;
                        }
                    case "X":
                        {
                            return UserManagementEnum.Exit;
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

        public virtual void Add()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Delete()
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
    }
}
