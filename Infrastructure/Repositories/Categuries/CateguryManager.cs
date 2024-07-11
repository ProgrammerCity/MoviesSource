using Domain.IRepositories.Categuries;
using Domain.Models.Catequries;
using DomainShared.ViewModels.Categuries;
using EntityCore.Data;
using EntityFreamewoerkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Categuries
{
    public class CateguryManager(MainApplicationDbContext context) : Repository<Categury>(context), ICateguryRepository
    {
        public async Task<List<CateguryListViewModel>> GetCateguryList()
        {
            return await TableNoTracking.Select(t => new CateguryListViewModel()
            {
                Id = t.Id,
                Name = t.Name
            }).ToListAsync();
        }

        public async Task<(string error, bool isSuccess)> CreateCategury(string name)
        {
            try
            {
                await Entities.AddAsync(new Categury(Guid.NewGuid(), name));
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

        public async Task<(string error, bool isSuccess)> UpdataCategury(Guid catId, string name)
        {
            var gen = await Entities.FirstOrDefaultAsync(t => t.Id == catId);

            if (gen == null)
                return new("دسته مورد نظر یافت نشد!!!", false);

            try
            {
                gen.SetName(name);
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
