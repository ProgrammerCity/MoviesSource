namespace DomainShared.ViewModels.Genres
{
    public record GenresListViewModel
    {
        public Guid Id { get; set; }
        public string Titele { get; set; } = default!;
        public bool Selected { get; set; } = false;
    }
}
