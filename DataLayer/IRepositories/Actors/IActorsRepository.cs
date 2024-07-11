using Domain.Entities.Actors;
using DomainShared.ViewModels.Actors;

namespace Domain.IRepositories.Actors
{
    public interface IActorsRepository : IRepository<Actor>
    {
       Task<List<ActorsListViewModel>> GetActorsList();
    }
}
