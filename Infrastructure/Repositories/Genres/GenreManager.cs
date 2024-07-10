using Domain.IRepositories.Genres;
using Domain.Models.Genres;
using DomainShared.ViewModels.Genres;
using EntityCore.Data;
using EntityFreamewoerkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Genres
{
    public class GenreManager(MainApplicationDbContext context) : Repository<Genre>(context), IGenresRepository
    {
        public async Task<List<GenresListViewModel>> GetGenresList()
        {
            return await TableNoTracking.Select(t => new GenresListViewModel(t.Id, t.Titele)).ToListAsync();
        }

        public async Task<(string error, bool isSuccess)> CreateGenre(string titele)
        {
            try
            {
                await Entities.AddAsync(new Genre(Guid.NewGuid(), titele));
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

        public async Task<(string error, bool isSuccess)> UpdataGenre(Guid genreId, string titele)
        {
            var gen = await Entities.FirstOrDefaultAsync(t => t.Id == genreId);

            if (gen == null)
                return new("ژانر مورد نظر یافت نشد!!!", false);

            try
            {
                gen.SetTitele(titele);
                Entities.Update(gen);
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
