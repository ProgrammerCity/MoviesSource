namespace Domain.Abstractions
{
    public interface IAuditedEntity<TKey> : IEntities
    {
        DateTime CreationTime { get; set; }
        Guid CreatorId { get; set; }
        DateTime? LastModificationTime { get; set; }
        Guid? LastModifireId { get; set; }
    }
}
