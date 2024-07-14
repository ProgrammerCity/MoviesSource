namespace DomainShared.ViewModels.Genres
{
    public class GenresListViewModel
    {
        public Guid Id { get; set; }
        public string Titele { get; set; } = default!;
        public bool Selected { get; set; } = false;
    }
}
