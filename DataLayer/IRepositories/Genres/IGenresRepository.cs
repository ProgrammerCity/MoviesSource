using Domain.Models.Genres;
using DomainShared.ViewModels.Genres;

namespace Domain.IRepositories.Genres
{
    public interface IGenresRepository : IRepository<Genre>
    {
        Task<List<GenresListViewModel>> GetGenresList();

        Task<(string error, bool isSuccess)> CreateGenre(string titele);

        Task<(string error, bool isSuccess)> UpdataGenre(Guid genreId,
             string titele);
    }
}
