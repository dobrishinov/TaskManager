namespace TaskManager.Entites
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TimeEntity : BaseEntity
    {
        public int TaskId { get;  set;}
        public int EstimatedTime { get; set; }
        public DateTime LastChange { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
