using Domain.Abstractions;
using Domain.Models.Movies;
using DomainShared.ViewModels.Movies;
using DomainShared.ViewModels.PagedResult;

namespace Domain.Movies
{
    public interface IMoviesRepository : IRepository<Movie>
    {
        Task<PagedResulViewModel<MoviesListViewModel>> GetMoviesList(string name,
            Guid CatequryId,
            Guid GenreId,
            int pageNum ,
            int pageCount);
        Task Create(string name, byte rate, Guid categuryId, Guid gereId);
    }
}
