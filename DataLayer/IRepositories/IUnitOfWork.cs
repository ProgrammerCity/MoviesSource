using Domain.IRepositories.Actors;
using Domain.IRepositories.Categuries;
using Domain.IRepositories.Genres;
using Domain.Movies;

namespace Domain.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IMoviesRepository MoviesRepository { get; }
        IGenresRepository GenresRepository { get; }
        IActorsRepository ActorsRepository{ get; }
        ICateguryRepository CateguryRepository{ get; }
        Task SaveChangesAsync();
    }
}
