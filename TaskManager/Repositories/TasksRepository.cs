﻿namespace TaskManager.Repositories
{
    using System;
    using TaskManager.Entites;
    using System.IO;
    using System.Collections.Generic;

    class TasksRepository : BaseController<TaskEntity>
    {
        public TasksRepository(string pathToFile)
            : base(pathToFile)
        {
        }

        protected override void WriteItemToStream(StreamWriter sw, TaskEntity item)
        {
            sw.WriteLine(item.ParentId);
            sw.WriteLine(item.Title);
            sw.WriteLine(item.ResponsibleUsers);
            sw.WriteLine(item.Creator);
            sw.WriteLine(item.Status);
            sw.WriteLine(item.LastChange);
            sw.WriteLine(item.CreateTime);
            sw.WriteLine(item.Time);
            sw.WriteLine(item.ContentId);
        }

        protected override void ReadItemFromStream(StreamReader sr, TaskEntity item)
        {
            item.ParentId = Convert.ToInt32(sr.ReadLine());
            item.Title = sr.ReadLine();
            item.ResponsibleUsers = Convert.ToInt32(sr.ReadLine());
            item.Creator = sr.ReadLine();
            item.Status = sr.ReadLine();
            item.LastChange = Convert.ToDateTime(sr.ReadLine());
            item.CreateTime = Convert.ToDateTime(sr.ReadLine());
            item.Time = Convert.ToDateTime(sr.ReadLine());
            item.ContentId = Convert.ToInt32(sr.ReadLine());

        }

        public List<TaskEntity> GetAll(int parentId)
        {
            return GetAll().FindAll(c => c.ParentId == parentId);
        }
    }
}