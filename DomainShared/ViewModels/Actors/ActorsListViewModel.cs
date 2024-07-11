namespace DomainShared.ViewModels.Actors
{
    public class ActorsListViewModel
    {
        public string Name { get; set; } = default!;
        public string NickName { get; set; } = default!;
        public string Path { get; set; } = default!;
        public bool Selected { get; set; } = false;
        public Guid Id { get; set; }
    }
}
