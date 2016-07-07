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

    class TimeRepository : BaseController<TimeEntity>
    {
        public TimeRepository(string pathToFile)
            : base(pathToFile)
        {
        }

        protected override void WriteItemToStream(StreamWriter sw, TimeEntity item)
        {
            sw.WriteLine(item.EstimatedTime);
            sw.WriteLine(item.LastChange.ToString("dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture));
            sw.WriteLine(item.CreateTime.ToString("dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture));
        }

        protected override void ReadItemFromStream(StreamReader sr, TimeEntity item)
        {
            item.EstimatedTime = Convert.ToInt32(sr.ReadLine());
            item.LastChange = DateTime.ParseExact(sr.ReadLine(), "dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture);
            item.CreateTime = DateTime.ParseExact(sr.ReadLine(), "dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture);
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
                    item.TaskId = Convert.ToInt32(sr.ReadLine());
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
