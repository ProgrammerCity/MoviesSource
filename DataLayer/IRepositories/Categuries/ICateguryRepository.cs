using Domain.Abstractions;
using Domain.Models.Catequries;
using DomainShared.ViewModels.Categuries;
using DomainShared.ViewModels.PagedResult;

namespace Domain.IRepositories.Categuries
{
    public interface ICateguryRepository : IRepository<Categury>
    {
        Task<List<CateguryListViewModel>> GetCateguryList();

        Task<(string error, bool isSuccess)> CreateCategury(string name);

        Task<(string error, bool isSuccess)> UpdataCategury(Guid catId,
             string name);
    }
}
