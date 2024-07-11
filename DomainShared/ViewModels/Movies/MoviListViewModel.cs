﻿namespace DomainShared.ViewModels.Movies
{
    public record MoviListViewModel(Guid Id,
        string Name,
        string DirectorName,
        string GenreTitele,
        float Rate,
        int ConstructionYear);
}
