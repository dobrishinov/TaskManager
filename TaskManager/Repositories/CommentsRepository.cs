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

    class CommentsRepository : BaseRepository<CommentEntity>
    {
        public CommentsRepository(string pathToFile) 
            : base(pathToFile)
        {
        }

        protected override void WriteItemToStream(StreamWriter sw, CommentEntity item)
        {
            sw.WriteLine(item.CreatorId);
            sw.WriteLine(item.TaskId);
            sw.WriteLine(item.Comment);
            sw.WriteLine(item.CreateDate);
        }

        protected override void ReadItemFromStream(StreamReader sr, CommentEntity item)
        {
            item.CreatorId = Convert.ToInt32(sr.ReadLine());
            item.TaskId = Convert.ToInt32(sr.ReadLine());
            item.Comment = sr.ReadLine();
            item.CreateDate = DateTime.Parse(sr.ReadLine());
        }

        public virtual List<CommentEntity> GetAll(int taskId)
        {
            List<CommentEntity> result = new List<CommentEntity>();

            FileStream fs = new FileStream("comments.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    CommentEntity item = new CommentEntity();
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
