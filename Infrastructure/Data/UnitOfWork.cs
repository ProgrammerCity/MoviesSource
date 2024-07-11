using Domain.IRepositories;
using Domain.IRepositories.Actors;
using Domain.IRepositories.Categuries;
using Domain.IRepositories.Genres;
using Domain.Movies;
using EntityCore.Data;
using EntityFreamewoerkCore.Movies;
using Infrastructure.Repositories.Actors;
using Infrastructure.Repositories.Categuries;
using Infrastructure.Repositories.Genres;

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
        private ICateguryRepository? _categuryRepository;
        private IGenresRepository? _genresRepository;
        private IActorsRepository? _actorsRepository;

        public IMoviesRepository MoviesRepository
        {
            get
            {
                _moviesRepository ??= new MoviesManager(_mainDb);
                return _moviesRepository;
            }
        }
        
        public ICateguryRepository CateguryRepository
        {
            get
            {
                _categuryRepository ??= new CateguryManager(_mainDb);
                return _categuryRepository;
            }
        }
        
        public IGenresRepository GenresRepository
        {
            get
            {
                _genresRepository ??= new GenreManager(_mainDb);
                return _genresRepository;
            }
        }
        
        public IActorsRepository ActorsRepository
        {
            get
            {
                _actorsRepository ??= new ActorsManager(_mainDb);
                return _actorsRepository;
            }
        }

        public async void Dispose() => await _mainDb.DisposeAsync();

        public async Task SaveChangesAsync()
        {
            await _mainDb.SaveChangesAsync();
        }
    }
}
