namespace TaskManager.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TaskManager.Entites;
    using TaskManager.Services;

    class TimeRepository : BaseRepository<TimeEntity>
    {
        public TimeRepository(string pathToFile)
            : base(pathToFile)
        {
        }

        protected override void WriteItemToStream(StreamWriter sw, TimeEntity item)
        {
            sw.WriteLine(item.TaskId);
            sw.WriteLine(item.EstimatedTime);
            sw.WriteLine(item.LastChange);
            sw.WriteLine(item.CreateTime);
        }

        protected override void ReadItemFromStream(StreamReader sr, TimeEntity item)
        {
            item.TaskId=Convert.ToInt32(sr.ReadLine());
            item.EstimatedTime = Convert.ToInt32(sr.ReadLine());
            item.LastChange = Convert.ToDateTime(sr.ReadLine());
            item.CreateTime = Convert.ToDateTime(sr.ReadLine());
        }

        public virtual List<TimeEntity> GetAll(int taskId)
        {
            List<TimeEntity> result = new List<TimeEntity>();

            FileStream fs = new FileStream("time.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    TimeEntity item = new TimeEntity();
                    item.Id = Convert.ToInt32(sr.ReadLine());
                    ReadItemFromStream(sr, item);
                    if (taskId == item.TaskId)
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
