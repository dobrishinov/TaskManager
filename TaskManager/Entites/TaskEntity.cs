﻿namespace TaskManager.Entites
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TaskEntity : BaseEntity
    {
        public int CreatorId { get; set; }
        public int ResponsibleUsers { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Creator { get; set; }
    }
}
