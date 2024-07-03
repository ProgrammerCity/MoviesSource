namespace DomainShared.ViewModels.Movies
{
    public record MoviesListViewModel(string name,
        string categuryId,
        string genreTitele,
        byte rate,
        Guid genreId,
        Guid catequryId);
}
