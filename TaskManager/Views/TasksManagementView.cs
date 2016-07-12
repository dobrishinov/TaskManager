namespace TaskManager.Views
{
    using TaskManager.Entites;
    using TaskManager.Repositories;
    using TaskManager.Services;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    class TasksManagementView : BaseView
    {
        public override void GetAll()
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

        public override void View()
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

            StatusRepository statusRepository = new StatusRepository("status.txt");
            //StatusEntity status = statusRepository.GetAll().Find(stat => stat.TaskId == commentId);
            StatusEntity status = null;

            foreach (var comment in comments)
            {
                status = statusRepository.GetAll().Find(stat => stat.CommentId == comment.Id);
                Console.WriteLine("Comment ID: " + comment.Id);
                Console.WriteLine("Comment: " + comment.Comment);
                Console.WriteLine("Comment create at: " + comment.CreateDate);
                Console.WriteLine("############################################");
                Console.WriteLine("Task status: " + status.Status);
                Console.WriteLine("############################################");
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("############################################");
                Console.ResetColor();
            }



            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Task found. Press Key To Return!");
            Console.ResetColor();

            Console.ReadKey(true);
        }

        public override void Add()
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("###############|Add New Task|################");
            Console.ResetColor();

            TaskEntity task = new TaskEntity();
            TimeEntity time = new TimeEntity();
            CommentEntity comment = new CommentEntity();
            StatusEntity status = new StatusEntity();

            Console.Write("Add Task Title: ");
            task.Title = Console.ReadLine();

            Console.Write("Add Task Content: ");
            task.Content = Console.ReadLine();

            task.Creator = Auth.LoggedUser.Username;

            Console.Write("Enter ID to Responsible User: ");
            task.ResponsibleUsers = int.Parse(Console.ReadLine());

            Console.Write("Task Status(Working/Idle/Done): ");
            status.Status = Console.ReadLine();

            
            Console.Write("Working time in Hours: ");
            time.EstimatedTime = Convert.ToInt32(Console.ReadLine());
            
            time.CreateTime = DateTime.Now;

            time.LastChange = DateTime.Now;

            Console.Write("Add Comment: ");
            comment.Comment = Console.ReadLine();
            comment.CreatorId = Auth.LoggedUser.Id;
            comment.TaskId = task.Id;
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

            status.TaskId = task.Id;
            status.CommentId = comment.Id;
            StatusRepository statusRepository = new StatusRepository("status.txt");
            statusRepository.Save(status);

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Task saved successfully!");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        public override void Update()
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

            Console.WriteLine("Estimate Time (in hours): " + times.EstimatedTime);
            Console.Write("Enter new Estimate Time (in hours): ");
            int time = int.Parse(Console.ReadLine());

            times.LastChange = DateTime.Now;


            if (!string.IsNullOrEmpty(title))
                task.Title = title;
            if (!string.IsNullOrEmpty(content))
                task.Content = content;
            //if (!string.IsNullOrEmpty(status))
            //    task.Status = status;
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

        public override void Delete()
        {
            TasksRepository tasksRepository = new TasksRepository("tasks.txt");
            TimeRepository timeRepository = new TimeRepository("time.txt");
            CommentsRepository commentRepository = new CommentsRepository("comments.txt");
            StatusRepository statusRepository = new StatusRepository("status.txt");

            Console.Clear();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("#########|Delete Task By ID|############");
            Console.ResetColor();

            Console.Write("Please enter task ID: ");
            int taskId = Convert.ToInt32(Console.ReadLine());

            TaskEntity task = tasksRepository.GetById(taskId);
            TimeEntity time = timeRepository.GetById(taskId);
            CommentEntity comment = commentRepository.GetById(taskId);
            StatusEntity status = statusRepository.GetById(taskId);

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
                statusRepository.Delete(status);
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Task deleted successfully!");
                Console.ResetColor();
            }
            Console.ReadKey(true);
        }

        public override void CommentAdd()
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("###############|Add New Comment|################");
            Console.ResetColor();

            TaskEntity task = new TaskEntity();
            CommentEntity comment = new CommentEntity();
            StatusEntity status = new StatusEntity();

            Console.Write("Please enter task ID: ");
            int commentId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("############################################");

            Console.Write("Write Comment: ");
            comment.Comment = Console.ReadLine();

            Console.Write("Task Status(Working/Idle/Done): ");
            status.Status = Console.ReadLine();

            comment.CreatorId = Auth.LoggedUser.Id;
            comment.TaskId = commentId;
            comment.CreateDate = DateTime.Now;

            CommentsRepository commentRepository = new CommentsRepository("comments.txt");
            commentRepository.Save(comment);

            status.TaskId = task.Id;
            status.CommentId = comment.Id;
            StatusRepository statusRepository = new StatusRepository("status.txt");
            statusRepository.Save(status);

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Comment and Status saved successfully!");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        public override void CommentEdit()
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

        public override void CommentDelete()
        {
            TasksRepository tasksRepository = new TasksRepository("tasks.txt");
            CommentsRepository commentRepository = new CommentsRepository("comments.txt");
            StatusRepository statusRepository = new StatusRepository("status.txt");

            Console.Clear();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("#########|Delete Comment By ID|############");
            Console.ResetColor();

            Console.Write("Please enter comment ID: ");
            int commentId = Convert.ToInt32(Console.ReadLine());

            CommentEntity comment = commentRepository.GetById(commentId);
            StatusEntity status = statusRepository.GetById(commentId);

            if (comment == null)
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

    }
}