using System.ComponentModel.DataAnnotations;

namespace Domain.Abstractions
{
    public abstract class Entity<TKey> : IEntities
    {
        [Key]
        public TKey Id { get; set; } 
        public DateTime? DeletionTime { get; set; }
        public Guid? DeleterId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime {  get; set; }
        public Guid CreatorId {  get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? LastModifireId { get; set; }
    }

    public abstract class Entity : Entity<int>
    {

    }
}
