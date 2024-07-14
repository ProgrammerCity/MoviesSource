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
            Guid? genreId,
            int? constructionYear,
            int pageNum,
            int pageCount);

        Task<List<MoviListViewModel>> GetDashboardMovies();

        Task<(string error, bool isSuccess)> Create(string name,
            string bannerPath,
             float rate,
             List<Guid> categuries,
             List<Guid> genres,
             List<Guid> actor,
             int constructionYear,
             string directorName);

        Task<(string error, bool isSuccess)> UpdataMovie(Guid movieId,
            string name,
            string bannerPath,
            float rate,
            List<Guid> categuries,
            List<Guid> genres,
            List<Guid> actors,
            int constructionYear,
            string directorName);

        Task<(string error, bool isSuccess)> DeleteMovie(Guid movieId);
    }
}
