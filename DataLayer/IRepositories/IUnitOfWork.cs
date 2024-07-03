using Domain.Movies;

namespace Domain.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IMoviesRepository MoviesRepository { get; }
        Task SaveChangesAsync();
    }
}
