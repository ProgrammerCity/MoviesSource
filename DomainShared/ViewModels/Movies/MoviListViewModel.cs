namespace DomainShared.ViewModels.Movies
{
    public record MoviListViewModel(string Name,
        string DirectorName,
        List<string> CateguryName,
        List<string> GenreTitele,
        float Rate);
}
