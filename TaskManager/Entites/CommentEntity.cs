namespace TaskManager.Entites
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CommentEntity : BaseEntity
    {
        public int CreatorId { get; set; }
        public int TaskId { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
