namespace Domain.Abstractions
{
    public interface ISoftDeleted
    {
        bool IsDeleted { get; set; }
        DateTime? DeletionTime { get; set; }
        Guid? DeleterId { get; set; }
    }
}
