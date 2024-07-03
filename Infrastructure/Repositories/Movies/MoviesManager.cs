using Domain.Models.Movies;
using Domain.Movies;
using DomainShared.ViewModels.Movies;
using DomainShared.ViewModels.PagedResult;
using EntityCore.Data;
using EntityFreamewoerkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EntityFreamewoerkCore.Movies
{
    public class MoviesManager(MainApplicationDbContext context) : Repository<Movie>(context), IMoviesRepository
    {
        public async Task Create(string name, 
            byte rate,
            Guid categuryId, 
            Guid gereId)
        {
            await Entities.AddAsync(new Movie(Guid.NewGuid(),categuryId,name,gereId,rate));
        }

        public async Task<PagedResulViewModel<MoviesListViewModel>> GetMoviesList(string name,
            Guid CatequryId,
            Guid GenreId,
            int pageNum,
            int pageCount )
        {
            var movies = TableNoTracking
                .Include(c => c.Categury)
                .Include(c => c.Genre)
                .Where(t => string.IsNullOrEmpty(name) || t.Name.Contains(name))
                .Select(t => new MoviesListViewModel(t.Name, t.Categury.Name, t.Genre.Titele, t.Rate, t.GenreId, t.CateguryId))
            .AsQueryable();

            return new PagedResulViewModel<MoviesListViewModel>(
                await movies.CountAsync(),
                pageCount, 
                pageNum, 
                await movies.Skip(--pageNum).Take(pageCount).ToListAsync());
        }
    }
}
