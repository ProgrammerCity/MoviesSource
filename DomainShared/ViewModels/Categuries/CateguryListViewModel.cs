namespace DomainShared.ViewModels.Categuries
{
    public record CateguryListViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public bool Selected { get; set; } = false;
    }
}
