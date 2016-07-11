namespace TaskManager.Repositories
{
    using System;
    using TaskManager.Entites;
    using TaskManager.Services;
    using System.IO;
    using System.Collections.Generic;

    class TasksRepository : BaseRepository<TaskEntity>
    {
        public TasksRepository(string pathToFile)
            : base(pathToFile)
        {
        }

        protected override void WriteItemToStream(StreamWriter sw, TaskEntity item)
        {
            sw.WriteLine(item.CreatorId);
            sw.WriteLine(item.ResponsibleUsers);
            sw.WriteLine(item.Title);
            sw.WriteLine(item.Content);
            sw.WriteLine(item.Creator);
            //sw.WriteLine(item.Status);
        }

        protected override void ReadItemFromStream(StreamReader sr, TaskEntity item)
        {
            item.CreatorId = Convert.ToInt32(sr.ReadLine());
            item.ResponsibleUsers = Convert.ToInt32(sr.ReadLine());
            item.Title = sr.ReadLine();
            item.Content = sr.ReadLine();
            item.Creator = sr.ReadLine();
            //item.Status = sr.ReadLine();
        }

        public virtual List<TaskEntity> GetAll(int CreatorId)
        {
            List<TaskEntity> result = new List<TaskEntity>();

            FileStream fs = new FileStream("tasks.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    TaskEntity item = new TaskEntity();
                    item.Id = Convert.ToInt32(sr.ReadLine());
                    ReadItemFromStream(sr, item);
                    if (item.CreatorId == Auth.LoggedUser.Id || item.ResponsibleUsers == Auth.LoggedUser.Id)
                    {
                        result.Add(item);
                    }

                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }
            return result;
        }



    }
}