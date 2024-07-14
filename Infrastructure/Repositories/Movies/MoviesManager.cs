using Domain.Entities.Actors;
using Domain.Models.Catequries;
using Domain.Models.Genres;
using Domain.Models.Movies;
using Domain.Movies;
using DomainShared.ViewModels.Movies;
using DomainShared.ViewModels.PagedResult;
using EntityCore.Data;
using EntityFreamewoerkCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EntityFreamewoerkCore.Movies
{
    public class MoviesManager(MainApplicationDbContext context) : Repository<Movie>(context), IMoviesRepository
    {
        public async Task<MoviDetailsViewModel> GetMovieById(Guid id)
        {
            var movie = await TableNoTracking
                .Include(t => t.Categuries)
                .Include(t => t.Genres)
                .Include(t => t.Actors)
                .FirstAsync(t => t.Id == id);
            return new MoviDetailsViewModel(
                           movie.Id,
                           movie.Categuries.Select(c => c.Id).ToList(),
                           movie.Genres.Select(c => c.Id).ToList(),
                           movie.Actors.Select(c => c.Id).ToList(),
                           movie.Name,
                           movie.BannerPath,
                           movie.DirectorName,
                           movie.Rate,
                           movie.ConstructionYear);

        }

        public async Task<PagedResulViewModel<MoviListViewModel>> GetMoviesList(string name,
            Guid? genreId,
            int? constructionYear,
            int pageNum,
            int pageCount)
        {
            var q = TableNoTracking
                          .Where(t => string.IsNullOrEmpty(name) || t.Name.Contains(name))
                          .Where(t => constructionYear == null || t.ConstructionYear == constructionYear)
                          .Where(t => genreId == null || t.Genres.Any(t => t.Id == genreId))
                          .Select(t => new MoviListViewModel(t.Id,
                              t.Name,
                              t.BannerPath,
                              t.DirectorName,
                              string.Join(" , ", t.Genres.Select(g => g.Titele).ToList()),
                              t.Rate,
                              t.ConstructionYear)).AsQueryable();

            return new PagedResulViewModel<MoviListViewModel>(
                await q.CountAsync(),
                pageCount,
                pageNum,
                await q.Skip(--pageNum * pageCount).Take(pageCount).ToListAsync());
        }

        public async Task<List<MoviListViewModel>> GetDashboardMovies()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return await TableNoTracking
                          .Select(t => new MoviListViewModel(t.Id,
                              t.Name,
                              path + t.BannerPath,
                              t.DirectorName,
                              string.Join(" , ", t.Genres.Select(g => g.Titele).ToList()),
                              t.Rate,
                              t.ConstructionYear)).ToListAsync();
        }


        public async Task<(string error, bool isSuccess)> Create(string name,
                    string bannerPath,
                    float rate,
                    List<Guid> categuries,
                    List<Guid> genres,
                    List<Guid> actor,
                    int constructionYear,
                    string directorName)
        {
            var cats = await DbContext.Set<Categury>().Where(t => categuries.Contains(t.Id)).ToListAsync();
            var gens = await DbContext.Set<Genre>().Where(t => genres.Contains(t.Id)).ToListAsync();
            var acts = await DbContext.Set<Actor>().Where(t => actor.Contains(t.Id)).ToListAsync();

            try
            {
                await Entities.AddAsync(new Movie(Guid.NewGuid(), cats, name, bannerPath, gens, acts, rate, constructionYear, directorName));
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
            string bannerPath,
            float rate,
            List<Guid> categuries,
            List<Guid> genres,
            List<Guid> actors,
            int constructionYear,
            string directorName)
        {
            var movie = await Entities
                .Include(t => t.Categuries)
                .Include(t => t.Genres)
                .Include(t => t.Actors)
                .FirstOrDefaultAsync(t => t.Id == movieId);

            if (movie == null)
                return new("فیلم مورد نظر یافت نشد!!!", false);

            var cats = await DbContext.Set<Categury>().Where(t => categuries.Contains(t.Id)).ToListAsync();
            var gens = await DbContext.Set<Genre>().Where(t => genres.Contains(t.Id)).ToListAsync();
            var acts = await DbContext.Set<Actor>().Where(t => actors.Contains(t.Id)).ToListAsync();
            try
            {
                movie.SetName(name);
                movie.SetBanner(bannerPath);
                movie.SetGenre(gens);
                movie.SetCateguries(cats);
                movie.SetActors(acts);
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

        public async Task<(string error, bool isSuccess)> DeleteMovie(Guid movieId)
        {
            try
            {
                var movie = await Entities
                     .FirstOrDefaultAsync(t => t.Id == movieId);

                if (movie == null)
                    return new("فیلم مورد نظر یافت نشد!!!", false);

                Entities.Remove(movie);
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
