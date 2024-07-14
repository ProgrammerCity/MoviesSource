using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using System.Reflection;

namespace EntityCore.Data
{
    public class MainApplicationDbContext : DbContext
    {
        private static readonly MethodInfo ConfigurePropertiesMethodInfo = typeof(MainApplicationDbContext)
        .GetMethod(nameof(ConfigureProperties),
        BindingFlags.Instance | BindingFlags.NonPublic)!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer($"Data Source=192.168.10.242\\DESKTOP-L3HJTMF\\SQLEXPRESS,1434;Database=TestCore;TrustServerCertificate=True;User Id=MyLogIn;Password=P123a@h;");
            optionsBuilder.UseSqlServer($"Data Source=(LocalDb)\\MSSQLLocalDB;Database=TestCore;TrustServerCertificate=True;Integrated Security=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entitiesAssembly = typeof(IEntities).Assembly;
            modelBuilder.RegisterAllEntities<IEntities>(entitiesAssembly);
            modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
            modelBuilder.AddSequentialGuidForIdConvention();
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                ConfigurePropertiesMethodInfo
                    .MakeGenericMethod(entityType.ClrType)
                    .Invoke(this, new object[] { modelBuilder, entityType });
            }
            modelBuilder.ConfigureNovinDbContext();
        }

        protected virtual void ConfigureProperties<TEntity>(ModelBuilder modelBuilder, IMutableEntityType mutableEntityType)
                    where TEntity : class
        {
            if (mutableEntityType.IsOwned())
            {
                return;
            }

            if (!typeof(IEntities).IsAssignableFrom(typeof(TEntity)))
            {
                return;
            }

            ConfigureGlobalFilters<TEntity>(modelBuilder, mutableEntityType);
        }

        protected virtual void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder, IMutableEntityType mutableEntityType)
     where TEntity : class
        {
            if (mutableEntityType.BaseType == null && ShouldFilterEntity<TEntity>(mutableEntityType))
            {
                var filterExpression = CreateFilterExpression<TEntity>();
                if (filterExpression != null)
                {
                    modelBuilder.Entity<TEntity>().HasQueryFilter(filterExpression);
                }
            }
        }

        protected virtual bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType) where TEntity : class
        {
            if (typeof(ISoftDeleted).IsAssignableFrom(typeof(TEntity)))
            {
                return true;
            }

            return false;
        }

        protected virtual Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
            where TEntity : class
        {
            Expression<Func<TEntity, bool>> expression = null;

            if (typeof(ISoftDeleted).IsAssignableFrom(typeof(TEntity)))
            {
                expression = e => !EF.Property<bool>(e, "IsDeleted");
            }

            return expression;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            HandleSoftDelete();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void HandleSoftDelete()
        {
            var entities = ChangeTracker.Entries<IEntities>()
                                .Where(e => e.State == EntityState.Deleted || e.State == EntityState.Modified || e.State == EntityState.Added);
            foreach (var entity in entities)
            {
                switch (entity.State)
                {
                    case EntityState.Deleted:
                        if (entity.Entity is ISoftDeleted)
                        {
                            entity.State = EntityState.Modified;
                            var et = entity.Entity as ISoftDeleted;
                            et.DeletionTime = DateTime.Now;
                            et.DeleterId = Guid.Empty;
                            et.IsDeleted = true;
                        }
                        break;
                    case EntityState.Modified:
                        var book = entity.Entity;
                        book.LastModifireId = Guid.Empty;
                        book.LastModificationTime = DateTime.Now;
                        break;
                    case EntityState.Added:
                        var boook = entity.Entity;
                        boook.CreatorId = Guid.Empty;
                        boook.CreationTime = DateTime.Now;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
