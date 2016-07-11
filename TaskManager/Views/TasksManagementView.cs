namespace TaskManager.Views
{
    using TaskManager.Entites;
    using TaskManager.Repositories;
    using TaskManager.Services;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    public class TasksManagementView : TaskEntity
    {
        public void Show()
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

        protected TaskManagementEnum RenderMenu()
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

        private void GetAll()
        {
            Console.Clear();

            TasksRepository tasksRepository = new TasksRepository("tasks.txt");
            List<TaskEntity> tasks = tasksRepository.GetAll(Auth.LoggedUser.Id);

            TimeRepository timeRepository = new TimeRepository("time.txt");

            CommentsRepository commentRepository = new CommentsRepository("comments.txt");

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("#################|Get ALL|##################");
            Console.ResetColor();

            foreach (var task in tasks)
            {
                Console.WriteLine("Task ID: " + task.Id);
                Console.WriteLine("Task Title: " + task.Title);
                Console.WriteLine("Tasks Content: " + task.Content);
                Console.WriteLine("Task create by: " + task.Creator);
                Console.WriteLine("############################################");
                Console.WriteLine("Task status: " + task.Status);
                Console.WriteLine("Task response by: " + task.ResponsibleUsers);

                Console.WriteLine("############################################");

                List<TimeEntity> times = timeRepository.GetAll(task.Id);

                foreach (var time in times)
                {

                    Console.WriteLine("Creation Date: " + time.CreateTime);
                    Console.WriteLine("Last Change Date: " + time.LastChange);
                    Console.WriteLine("############################################");

                    Console.WriteLine("Estimate Time to task (in hours): " + time.EstimatedTime);

                    Console.WriteLine("############################################");

                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("##################COMMENTS##################");
                    Console.ResetColor();
                }

                List<CommentEntity> comments = commentRepository.GetAll(task.Id);

                foreach (var comment in comments)
                {

                    Console.WriteLine("Comment: " + comment.Comment);
                    Console.WriteLine("Comment create at: " + comment.CreateDate);

                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("############################################");
                    Console.ResetColor();
                }
            }
            Console.ReadKey(true);
        }

        private void View()
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("################|View By ID|################");
            Console.ResetColor();

            Console.Write("Please enter task ID: ");
            int taskId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("############################################");

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

            TimeRepository timeRepository = new TimeRepository("time.txt");
            List<TimeEntity> times = timeRepository.GetAll(task.Id);

            CommentsRepository commentRepository = new CommentsRepository("comments.txt");
            List<CommentEntity> comments = commentRepository.GetAll(task.Id);

            Console.WriteLine("Task ID: " + task.Id);
            Console.WriteLine("Task Title: " + task.Title);
            Console.WriteLine("Tasks Content: " + task.Content);
            Console.WriteLine("Task create by: " + task.Creator);
            Console.WriteLine("############################################");
            Console.WriteLine("Task status: " + task.Status);
            Console.WriteLine("Task response by: " + task.ResponsibleUsers);
            Console.WriteLine("############################################");

            foreach (var time in times)
            {
                Console.WriteLine("Creation Date: " + time.CreateTime);
                Console.WriteLine("Last Change Date: " + time.LastChange);
                Console.WriteLine("############################################");

                Console.WriteLine("Estimate Time (in hours): " + time.EstimatedTime);

                Console.WriteLine("############################################");

                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("##################COMMENTS##################");
                Console.ResetColor();
            }

            foreach (var comment in comments)
            {
                Console.WriteLine("Comment ID: " + comment.Id);
                Console.WriteLine("Comment: " + comment.Comment);
                Console.WriteLine("Comment create at: " + comment.CreateDate);

                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("############################################");
                Console.ResetColor();
            }



            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Task found. Press Key To Return!");
            Console.ResetColor();

            Console.ReadKey(true);
        }

        private void CommentAdd()
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("###############|Add New Comment|################");
            Console.ResetColor();

            //Console.Write("Please enter task ID: ");
            //int taskId = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("############################################");

            //TasksRepository tasksRepository = new TasksRepository("tasks.txt");
            //TaskEntity task = tasksRepository.GetById(taskId);

            //if (task == null)
            //{
            //    Console.Clear();
            //    Console.BackgroundColor = ConsoleColor.Red;
            //    Console.WriteLine("Task not found. Press Key To Return!");
            //    Console.ResetColor();
            //    Console.ReadKey(true);
            //    return;
            //}

            //TimeRepository timeRepository = new TimeRepository("time.txt");
            //TimeEntity times = timeRepository.GetById(taskId);

            //CommentsRepository commentRepository = new CommentsRepository("comments.txt");
            //CommentEntity comments = commentRepository.GetById(taskId);



            TaskEntity task = new TaskEntity();
            CommentEntity comment = new CommentEntity();
            StatusEntity status = new StatusEntity();
            //TimeEntity time = new TimeEntity();
            //CommentEntity comment = new CommentEntity();

            Console.Write("Please enter task ID: ");
            int commentId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("############################################");

            //Console.Write("Add Comment: ");
            //comment.Comment = Console.ReadLine();
            //comment.CreatorId = Auth.LoggedUser.Id;
            //comment.TaskId = status.Id;
            //comment.CreateDate = DateTime.Now;

            Console.Write("Write Comment: ");
            comment.Comment = Console.ReadLine();

            Console.Write("Task Status(Working/Idle/Done): ");
            status.Status = Console.ReadLine();

            //Console.Write("Working time in Hours: ");
            //time.EstimatedTime = Convert.ToInt32(Console.ReadLine());

            //time.CreateTime = DateTime.Now;

            //time.LastChange = DateTime.Now;

            comment.CreatorId = Auth.LoggedUser.Id;
            comment.TaskId = commentId;
            comment.CreateDate = DateTime.Now;
            status.TaskId = commentId;

            CommentsRepository commentRepository = new CommentsRepository("comments.txt");
            commentRepository.Save(comment);
            StatusRepository statusRepository = new StatusRepository("status.txt");
            statusRepository.Save(status);
            
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Comment and Status saved successfully!");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        private void CommentEdit()
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("###############|Edit Comment|################");
            Console.ResetColor();

            Console.Write("Please enter comment ID: ");
            int commentId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("############################################");

            CommentsRepository commentsRepository = new CommentsRepository("comments.txt");
            CommentEntity comments = commentsRepository.GetById(commentId);
            StatusRepository statusRepository = new StatusRepository("status.txt");
            StatusEntity status = statusRepository.GetById(commentId);

            if (comments == null)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Comment not found. Press Key To Return!");
                Console.ResetColor();
                Console.ReadKey(true);
                return;
            }
            
            Console.WriteLine("Old comment content: " + comments.Comment);
            Console.Write("New comment content: ");
            string comment = Console.ReadLine();

            Console.WriteLine("Task Status: " + status.Status);
            Console.Write("New Task Status(Working/Idle/Done): ");
            string taskStatus = Console.ReadLine();

            if (!string.IsNullOrEmpty(comment))
                comments.Comment = comment;
            if (!string.IsNullOrEmpty(taskStatus))
                status.Status = taskStatus;

            commentsRepository.Save(comments);
            statusRepository.Save(status);

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Comments saved successfully. Press Key To Return!");
            Console.ResetColor();
            Console.ReadKey(true);

            Console.WriteLine("##########################################");
        }

        private void CommentDelete()
        {
            //Console.Clear();

            //Console.BackgroundColor = ConsoleColor.DarkRed;
            //Console.WriteLine("#########|Delete Task By ID|############");
            //Console.ResetColor();

            //TaskEntity task = new TaskEntity();
            //StatusEntity status = new StatusEntity();

            //Console.Write("Please enter task ID: ");
            //int taskId = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("############################################");

            ////TasksRepository tasksRepository = new TasksRepository("tasks.txt");
            //StatusRepository statusRepository = new StatusRepository("status.txt");
            ////TimeRepository timeRepository = new TimeRepository("time.txt");
            ////CommentsRepository commentRepository = new CommentsRepository("comments.txt");


            ////TimeEntity time = timeRepository.GetById(taskId);
            ////CommentEntity comment = commentRepository.GetById(taskId);
            //statusRepository.Delete(status);

            ////if (status == null || task.CreatorId != Auth.LoggedUser.Id)
            ////{
            ////    Console.BackgroundColor = ConsoleColor.Red;
            ////    Console.WriteLine("Task not found. Press Key To Return!");
            ////    Console.ResetColor();
            ////}
            ////else
            ////{
            ////    //tasksRepository.Delete(task);
            ////    ///timeRepository.Delete(time);
            ////    //commentRepository.Delete(comment);
            ////    Console.BackgroundColor = ConsoleColor.DarkGreen;
            ////    Console.WriteLine("Task deleted successfully!");
            ////    Console.ResetColor();
            ////}
            //Console.ReadKey(true);

            TasksRepository tasksRepository = new TasksRepository("tasks.txt");
            CommentsRepository commentRepository = new CommentsRepository("comments.txt");
            StatusRepository statusRepository = new StatusRepository("status.txt");

            Console.Clear();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("#########|Delete Comment By ID|############");
            Console.ResetColor();

            Console.Write("Please enter comment ID: ");
            int taskId = Convert.ToInt32(Console.ReadLine());

            TaskEntity task = tasksRepository.GetById(taskId);
            CommentEntity comment = commentRepository.GetById(taskId);
            StatusEntity status = statusRepository.GetById(taskId);

            if (task == null || task.CreatorId != Auth.LoggedUser.Id)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Comment not found. Press Key To Return!");
                Console.ResetColor();
            }
            else
            {
                commentRepository.Delete(comment);
                statusRepository.Delete(status);
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Comment deleted successfully!");
                Console.ResetColor();
            }
            Console.ReadKey(true);
        }

        private void Add()
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("###############|Add New Task|################");
            Console.ResetColor();

            TaskEntity task = new TaskEntity();
            TimeEntity time = new TimeEntity();
            CommentEntity comment = new CommentEntity();

            Console.Write("Add Task Title: ");
            task.Title = Console.ReadLine();

            Console.Write("Add Task Content: ");
            task.Content = Console.ReadLine();

            task.Creator = Auth.LoggedUser.Username;

            Console.Write("Enter ID to Responsible User: ");
            task.ResponsibleUsers = int.Parse(Console.ReadLine());

            Console.Write("Task Status(Working/Idle/Done): ");
            task.Status = Console.ReadLine();

            
            Console.Write("Working time in Hours: ");
            time.EstimatedTime = Convert.ToInt32(Console.ReadLine());
            
            time.CreateTime = DateTime.Now;

            time.LastChange = DateTime.Now;

            Console.Write("Add Comment: ");
            comment.Comment = Console.ReadLine();
            comment.CreatorId = Auth.LoggedUser.Id;
            comment.TaskId= task.Id;
            comment.CreateDate = DateTime.Now;


            task.CreatorId = Auth.LoggedUser.Id;
            TasksRepository tasksRepository = new TasksRepository("tasks.txt");
            tasksRepository.Save(task);

            time.TaskId = task.Id;
            TimeRepository timeRepository = new TimeRepository("time.txt");
            timeRepository.Save(time);

            comment.TaskId = task.Id;
            CommentsRepository commentsRepository = new CommentsRepository("comments.txt");
            commentsRepository.Save(comment);

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
            Console.WriteLine("############################################");

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

            TimeRepository timeRepository = new TimeRepository("time.txt");
            TimeEntity times = timeRepository.GetById(taskId);

            CommentsRepository commentRepository = new CommentsRepository("comments.txt");
            CommentEntity comments = commentRepository.GetById(taskId);

            Console.WriteLine("Editing task (" + task.Title + ") with ID: " + task.Id);

            Console.WriteLine("##########################################");

            Console.WriteLine("Old task title: " + task.Title);
            Console.Write("New Title: ");
            string title = Console.ReadLine();

            Console.WriteLine("Old task content: " + task.Content);
            Console.Write("New Content: ");
            string content = Console.ReadLine();

            Console.WriteLine("##########################################");

            Console.WriteLine("Status: " + task.Status);
            Console.Write("New Status(Working/Idle/Done): ");
            string status = Console.ReadLine();

            Console.WriteLine("##########################################");

            Console.WriteLine("Estimate Time (in hours): " + times.EstimatedTime);
            Console.Write("Enter new Estimate Time (in hours): ");
            int time = int.Parse(Console.ReadLine());

            times.LastChange = DateTime.Now;


            if (!string.IsNullOrEmpty(title))
                task.Title = title;
            if (!string.IsNullOrEmpty(content))
                task.Content = content;
            if (!string.IsNullOrEmpty(status))
                task.Status = status;
            //TODO maybe fix Estimate Time
            if (times.EstimatedTime != time)
                times.EstimatedTime = time;

            tasksRepository.Save(task);
            timeRepository.Save(times);

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Task saved successfully. Press Key To Return!");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        private void Delete()
        {
            TasksRepository tasksRepository = new TasksRepository("tasks.txt");
            TimeRepository timeRepository = new TimeRepository("time.txt");
            CommentsRepository commentRepository = new CommentsRepository("comments.txt");

            Console.Clear();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("#########|Delete Task By ID|############");
            Console.ResetColor();

            Console.Write("Please enter task ID: ");
            int taskId = Convert.ToInt32(Console.ReadLine());

            TaskEntity task = tasksRepository.GetById(taskId);
            TimeEntity time = timeRepository.GetById(taskId);
            CommentEntity comment = commentRepository.GetById(taskId);

            if (task == null || task.CreatorId != Auth.LoggedUser.Id)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Task not found. Press Key To Return!");
                Console.ResetColor();
            }
            else
            {
                tasksRepository.Delete(task);
                timeRepository.Delete(time);
                commentRepository.Delete(comment);
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Task deleted successfully!");
                Console.ResetColor();
            }
            Console.ReadKey(true);
        }


    }
}