using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface IEntities : ISoftDeleted
    {
        //TKey Id { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid CreatorId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? LastModifireId { get; set; }
    }
}
