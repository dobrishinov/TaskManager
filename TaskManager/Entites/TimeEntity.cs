namespace TaskManager.Entites
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TimeEntity : BaseEntity
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public int Hours { get; set; }
        public DateTime TimeTaken { get; set; }

    }
}
