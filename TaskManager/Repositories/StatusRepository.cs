namespace TaskManager.Repositories
{
    using System;
    using TaskManager.Entites;
    using TaskManager.Repositories;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;

    class StatusRepository : BaseRepository<StatusEntity>
    {
        public StatusRepository(string pathToFile) : base(pathToFile)
        {
        }

        protected override void WriteItemToStream(StreamWriter sw, StatusEntity item)
        {
            sw.WriteLine(item.TaskId);
            sw.WriteLine(item.CommentId);
            sw.WriteLine(item.Status);
        }

        protected override void ReadItemFromStream(StreamReader sr, StatusEntity item)
        {
            item.TaskId = Convert.ToInt32(sr.ReadLine());
            item.CommentId = Convert.ToInt32(sr.ReadLine());
            item.Status = sr.ReadLine();
        }

        public virtual List<StatusEntity> GetAll(int taskId)
        {
            List<StatusEntity> result = new List<StatusEntity>();

            FileStream fs = new FileStream("status.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    StatusEntity status = new StatusEntity();
                    status.Id = Convert.ToInt32(sr.ReadLine());
                    ReadItemFromStream(sr, status);
                    if (taskId == status.TaskId)
                    {
                        result.Add(status);
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
