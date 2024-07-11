using Domain.Entities.Actors;
using Domain.IRepositories.Actors;
using DomainShared.ViewModels.Categuries;
using EntityCore.Data;
using EntityFreamewoerkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Actors
{
    public class ActorsManager (MainApplicationDbContext context) : Repository<Actor>(context), IActorsRepository
    {

        public Task<List<ActorsListViewModel>> GetActorsList()
        {
            return TableNoTracking.Select(t => new ActorsListViewModel(t.Id, t.Name, t.Nickname, t.Path)).ToListAsync();
        }
    }
}
