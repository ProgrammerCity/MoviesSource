using Domain.Entities.Actors;
using Domain.IRepositories.Actors;
using DomainShared.ViewModels.Actors;
using EntityCore.Data;
using EntityFreamewoerkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Actors
{
    public class ActorsManager(MainApplicationDbContext context) : Repository<Actor>(context), IActorsRepository
    {

        public Task<List<ActorsListViewModel>> GetActorsList()
        {
            return TableNoTracking.Select(t => new ActorsListViewModel()
            {
                Id = t.Id,
                Name = t.Name,
                NickName = t.Nickname,
                Path = t.Path
            }).ToListAsync();
        }
    }
}
