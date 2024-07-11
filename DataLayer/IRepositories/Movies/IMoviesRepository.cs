using Domain.Abstractions;
using Domain.Models.Movies;
using DomainShared.ViewModels.Movies;
using DomainShared.ViewModels.PagedResult;

namespace Domain.Movies
{
    public interface IMoviesRepository : IRepository<Movie>
    {

        Task<MoviDetailsViewModel> GetMovieById(Guid id);

        Task<PagedResulViewModel<MoviListViewModel>> GetMoviesList(string name,
            float? rate,
            Guid? categuryId,
            Guid? gereId,
            int? constructionYear,
            string directorName,
            int pageNum,
            int pageCount);

        Task<(string error, bool isSuccess)> Create(string name,
             float rate,
             List<Guid> categuries,
             List<Guid> genres,
             int constructionYear,
             string directorName);

        Task<(string error, bool isSuccess)> UpdataMovie(Guid movieId,
             string name,
             float rate,
             List<Guid> categuries,
             List<Guid> genres,
             int constructionYear,
             string directorName);
        
        Task<(string error, bool isSuccess)> DeleteMovie(Guid movieId);
    }
}
