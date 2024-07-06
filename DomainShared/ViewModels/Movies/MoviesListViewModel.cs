namespace DomainShared.ViewModels.Movies
{
    public record MoviesListViewModel(string Name,
        string CateguryName,
        string GenreTitele,
        float Rate,
        Guid GenreId,
        Guid CatequryId);
}
