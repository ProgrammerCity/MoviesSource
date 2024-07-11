using Domain.Entities.Actors;
using DomainShared.ViewModels.Categuries;

namespace Domain.IRepositories.Actors
{
    public interface IActorsRepository : IRepository<Actor>
    {
       Task<List<ActorsListViewModel>> GetActorsList();
    }
}
