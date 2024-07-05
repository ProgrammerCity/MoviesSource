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
        public async Task<PagedResulViewModel<MoviesListViewModel>> GetMoviesList(string name,
            byte? rate,
            Guid? catequryId,
            Guid? genreId,
            int? constructionYear,
            string directorName,
            int pageNum,
            int pageCount)
        {
            var movies = TableNoTracking
                .Include(c => c.Categury)
                .Include(c => c.Genre)
                .Where(t => string.IsNullOrEmpty(name) || t.Name.Contains(name))
                .Where(t => string.IsNullOrEmpty(directorName) || t.DirectorName.Contains(directorName))
                .Where(t => genreId == null || t.GenreId == genreId)
                .Where(t => catequryId == null || t.CateguryId == catequryId)
                .Where(t => constructionYear == null || t.ConstructionYear == constructionYear)
                .Where(t => rate == null || t.Rate == rate)
                .Select(t => new MoviesListViewModel(t.Name, t.Categury.Name, t.Genre.Titele, t.Rate, t.GenreId, t.CateguryId))
            .AsQueryable();

            return new PagedResulViewModel<MoviesListViewModel>(
                await movies.CountAsync(),
                pageCount,
                pageNum,
                await movies.Skip(--pageNum).Take(pageCount).ToListAsync());
        }


        public async Task<(string error, bool isSuccess)> Create(string name,
                    byte rate,
                    Guid categuryId,
                    Guid gereId,
                    int constructionYear,
                    string directorName)
        {
            try
            {
                await Entities.AddAsync(new Movie(Guid.NewGuid(), categuryId, name, gereId, rate, constructionYear, directorName));
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException aex)
                {
                    return new(aex.Message, false);
                }
                return new(" خطا در اتصال به پایگاه داده code(59t46993)!!!", false);
            }
            return new(string.Empty, true);
        }

        //public async Task<(string error,bool isSuccess)> UpdataMovie()
        //{

        //}
    }
}
