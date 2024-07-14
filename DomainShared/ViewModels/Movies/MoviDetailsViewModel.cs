namespace DomainShared.ViewModels.Movies
{
    public record MoviDetailsViewModel(Guid Id,
        List<Guid> Catequries,
        List<Guid> Genres,
        List<Guid> Actors,
        string Name,
        string bannerPath,
        string DirectorName,
        float Rate,
        int ConstructionYear);
}
