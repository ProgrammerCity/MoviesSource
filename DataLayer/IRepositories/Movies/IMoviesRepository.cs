using Domain.Abstractions;
using Domain.Models.Movies;
using DomainShared.ViewModels.Movies;
using DomainShared.ViewModels.PagedResult;

namespace Domain.Movies
{
    public interface IMoviesRepository : IRepository<Movie>
    {
        Task<PagedResulViewModel<MoviesListViewModel>> GetMoviesList(string name,
            byte? rate,
            Guid? categuryId,
            Guid? gereId,
            int? constructionYear,
            string directorName,
            int pageNum,
            int pageCount);

        Task<(string error, bool isSuccess)> Create(string name, 
            byte rate,
            Guid categuryId, 
            Guid gereId,
            int constructionYear,
            string directorName);
    }
}
