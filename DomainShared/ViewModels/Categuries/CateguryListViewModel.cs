namespace DomainShared.ViewModels.Categuries
{
    public record CateguryListViewModel(Guid Id,
        string Titele);
    
    public record ActorsListViewModel(Guid Id,
        string Name,
        string Nickname,
        string Path);
}
