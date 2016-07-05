namespace TaskManager.Entites
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TaskEntity : BaseEntity
    {
        public int ParentId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int ResponsibleUsers { get; set; }
        public string Creator { get; set; }
        public string Status { get; set; }
        public DateTime LastChange { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime Time { get; set; }
    }
}
