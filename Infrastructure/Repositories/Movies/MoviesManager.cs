using Domain.Models.Catequries;
using Domain.Models.Genres;
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
            float? rate,
            Guid? catequryId,
            Guid? genreId,
            int? constructionYear,
            string directorName,
            int pageNum,
            int pageCount)

        {
            var query = (from mov in DbContext.Set<Movie>()
                          .AsNoTracking()
                          .Where(t => string.IsNullOrEmpty(name) || t.Name.Contains(name))
                          .Where(t => string.IsNullOrEmpty(directorName) || t.DirectorName.Contains(directorName))
                          .Where(t => genreId == null || t.GenreId == genreId)
                          .Where(t => catequryId == null || t.CateguryId == catequryId)
                          .Where(t => constructionYear == null || t.ConstructionYear == constructionYear)
                          .Where(t => rate == null || t.Rate == rate)

                  join cat in DbContext.Set<Categury>()
                           on mov.CateguryId equals cat.Id

                  join gen in DbContext.Set<Genre>()
                           on mov.GenreId equals gen.Id

                           select new MoviesListViewModel(
                               cat.Name,
                               cat.Name,
                               gen.Titele,
                               mov.Rate,
                               gen.Id,
                               cat.Id
                           )).AsQueryable();



            //var query = TableNoTracking
            //    .Where(t => string.IsNullOrEmpty(name) || t.Name.Contains(name))
            //    .Where(t => string.IsNullOrEmpty(directorName) || t.DirectorName.Contains(directorName))
            //    .Where(t => genreId == null || t.GenreId == genreId)
            //    .Where(t => catequryId == null || t.CateguryId == catequryId)
            //    .Where(t => constructionYear == null || t.ConstructionYear == constructionYear)
            //    .Where(t => rate == null || t.Rate == rate)
            //    .Select(t => new MoviesListViewModel(t.Name, "t.Categury.Name", "t.Genre.Titele", t.Rate, t.GenreId, t.CateguryId))
            //.AsQueryable();

            var count = await query.CountAsync();
            var movies = await query.Skip(--pageNum).Take(pageCount).ToListAsync();

            return new PagedResulViewModel<MoviesListViewModel>(
                await query.CountAsync(),
                pageCount,
                pageNum,
                await query.Skip(pageNum).Take(pageCount).ToListAsync());
        }


        public async Task<(string error, bool isSuccess)> Create(string name,
                    float rate,
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

        public async Task<(string error, bool isSuccess)> UpdataMovie(Guid movieId,
            string name,
            float rate,
            Guid categuryId,
            Guid gereId,
            int constructionYear,
            string directorName)
        {
            var movie = await Entities.FirstOrDefaultAsync(t => t.Id == movieId);

            if (movie == null)
                return new("فیلم مورد نظر یافت نشد!!!", false);

            try
            {
                movie.SetName(name);
                movie.SetGenre(gereId);
                movie.SetCategury(categuryId);
                movie.SetRate(rate);
                movie.SetCustructionYear(constructionYear);
                movie.SetDirectorNam(directorName);
                Entities.Update(movie);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException aex)
                {
                    return new(aex.Message, false);
                }
                return new(" خطا در اتصال به پایگاه داده code(77t46993)!!!", false);
            }
            return new(string.Empty, true);
        }
    }
}
