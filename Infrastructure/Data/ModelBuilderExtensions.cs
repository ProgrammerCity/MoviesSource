using Domain.Entities.Actors;
using Domain.Models.Catequries;
using Domain.Models.Genres;
using Domain.Models.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;
using System.Reflection.Emit;

namespace EntityCore.Data
{
    public static class ModelBuilderExtensions
    {

        public static void ConfigureNovinDbContext(this ModelBuilder builder)
        {

            builder.Entity<Movie>(b =>
            {
                b.HasIndex(t => t.Id);
                b.Property(t => t.Name).IsRequired().HasMaxLength(50);
                b.Property(t => t.DirectorName).IsRequired().HasMaxLength(50);
            });


            #region ManyToManyMovieGenre
            builder.Entity<Movie>()
              .HasMany(e => e.Genres)
              .WithMany(e => e.Movies)
              .UsingEntity("MovieGenre",
               l => l.HasOne(typeof(Genre)).WithMany().HasForeignKey("GenreId").HasPrincipalKey(nameof(Genre.Id)),
               r => r.HasOne(typeof(Movie)).WithMany().HasForeignKey("MvoieId").HasPrincipalKey(nameof(Movie.Id)),
               j => j.HasKey("GenreId", "MvoieId"));

            #endregion

            #region ManyToManyMovieCategury
            builder.Entity<MovieCategory>()
            .HasKey(mc => new { mc.MovieId, mc.CategoryId });

            builder.Entity<MovieCategory>()
            .HasOne(mc => mc.Movie)
            .WithMany(m => m.MovieCategory)
            .HasForeignKey(mc => mc.MovieId);

            //builder.Entity<Movie>()
            //  .HasMany(e => e.Categuries)
            //  .WithMany(e => e.Movies)
            //  .UsingEntity("MovieCategury",
            //   l => l.HasOne(typeof(Categury)).WithMany().HasForeignKey("CateguryId").HasPrincipalKey(nameof(Categury.Id)),
            //   r => r.HasOne(typeof(Movie)).WithMany().HasForeignKey("MvoieId").HasPrincipalKey(nameof(Movie.Id)),
            //   j => j.HasKey("CateguryId", "MvoieId"));
            #endregion
            
            #region ManyToManyMovieActors
            builder.Entity<Movie>()
              .HasMany(e => e.Actors)
              .WithMany(e => e.Movies)
              .UsingEntity("MovieActor",
               l => l.HasOne(typeof(Actor)).WithMany().HasForeignKey("ActorId").HasPrincipalKey(nameof(Actor.Id)),
               r => r.HasOne(typeof(Movie)).WithMany().HasForeignKey("MvoieId").HasPrincipalKey(nameof(Movie.Id)),
               j => j.HasKey("ActorId", "MvoieId"));
            #endregion


            builder.Entity<Categury>(b =>
            {
                b.HasIndex(t => t.Id);
                b.Property(t => t.Name).IsRequired();
            });

            builder.Entity<Genre>(b =>
            {
                b.HasIndex(t => t.Id);
                b.Property(t => t.Titele).IsRequired();
            });
            
            builder.Entity<Actor>(b =>
            {
                b.HasIndex(t => t.Id);
                b.Property(t => t.Name).IsRequired();
                b.Property(t => t.Nickname).IsRequired();
            });
        }

        /// <summary>
        /// Set NEWSEQUENTIALID() sql function for all columns named "Id"
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="mustBeIdentity">Set to true if you want only "Identity" guid fields that named "Id"</param>
        public static void AddSequentialGuidForIdConvention(this ModelBuilder modelBuilder)
        {
            modelBuilder.AddDefaultValueSqlConvention("Id", typeof(Guid), "NEWSEQUENTIALID()");
        }

        /// <summary>
        /// Set DefaultValueSql for sepecific property name and type
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="propertyName">Name of property wants to set DefaultValueSql for</param>
        /// <param name="propertyType">Type of property wants to set DefaultValueSql for </param>
        /// <param name="defaultValueSql">DefaultValueSql like "NEWSEQUENTIALID()"</param>
        public static void AddDefaultValueSqlConvention(this ModelBuilder modelBuilder, string propertyName, Type propertyType, string defaultValueSql)
        {
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                IMutableProperty property = entityType.GetProperties().SingleOrDefault(p => p.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
                if (property != null && property.ClrType == propertyType)
                    property.SetDefaultValueSql(defaultValueSql);
            }
        }

        /// <summary>
        /// Dynamicaly load all IEntityTypeConfiguration with Reflection
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="assemblies">Assemblies contains Entities</param>
        public static void RegisterEntityTypeConfiguration(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            MethodInfo applyGenericMethod = typeof(ModelBuilder).GetMethods().First(m => m.Name == nameof(ModelBuilder.ApplyConfiguration));

            IEnumerable<Type> types = assemblies.SelectMany(a => a.GetExportedTypes())
                .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic);

            foreach (Type type in types)
            {
                foreach (Type iface in type.GetInterfaces())
                {
                    if (iface.IsConstructedGenericType && iface.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                    {
                        MethodInfo applyConcreteMethod = applyGenericMethod.MakeGenericMethod(iface.GenericTypeArguments[0]);
                        applyConcreteMethod.Invoke(modelBuilder, new object[] { Activator.CreateInstance(type) });
                    }
                }
            }
        }

        /// <summary>
        /// Dynamicaly register all Entities that inherit from specific BaseType
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="baseType">Base type that Entities inherit from this</param>
        /// <param name="assemblies">Assemblies contains Entities</param>
        public static void RegisterAllEntities<BaseType>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            IEnumerable<Type> types = assemblies.SelectMany(a => a.GetExportedTypes())
                .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && typeof(BaseType).IsAssignableFrom(c));

            foreach (Type type in types)
                modelBuilder.Entity(type);
        }
    }
}
