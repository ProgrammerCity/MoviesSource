namespace Domain.Abstractions
{
    public abstract class AuditedEntity<TKey> : Entity<TKey>, IAuditedEntity<TKey>
    {
        public DateTime CreationTime { get; set; }
        public Guid CreatorId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? LastModifireId { get; set; }
    }

    public abstract class AuditedEntity : AuditedEntity<int>
    {

    }
}
