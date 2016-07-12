namespace TaskManager.Entites
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class StatusEntity : BaseEntity
    {
        public int TaskId { get; set; }
        public int CommentId { get; set; }
        public string Status { get; set; }
    }
}
