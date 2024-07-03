using Domain.IRepositories;
using Domain.Movies;
using EntityCore.Data;
using EntityFreamewoerkCore.Movies;

namespace EntityFreamewoerkCore.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly MainApplicationDbContext _mainDb;
        public UnitOfWork(MainApplicationDbContext mainContext)
        {
            _mainDb = mainContext;
        }

        private IMoviesRepository? _moviesRepository;

        public IMoviesRepository MoviesRepository
        {
            get
            {
                if (_moviesRepository == null)
                {
                    _moviesRepository = new MoviesManager(_mainDb);
                }
                return _moviesRepository;
            }
        }


        public async void Dispose() => await _mainDb.DisposeAsync();

        public async Task SaveChangesAsync()
        {
            await _mainDb.SaveChangesAsync();
        }
    }
}
