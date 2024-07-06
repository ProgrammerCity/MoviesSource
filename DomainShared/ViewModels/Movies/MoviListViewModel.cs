namespace DomainShared.ViewModels.Movies
{
    public record MoviListViewModel(string Name,
        string CateguryName,
        string GenreTitele,
        float Rate,
        Guid GenreId,
        Guid CatequryId);
}
