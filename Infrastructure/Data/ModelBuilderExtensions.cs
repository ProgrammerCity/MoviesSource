using Domain.Models.Catequries;
using Domain.Models.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;

namespace EntityCore.Data
{
    public static class ModelBuilderExtensions
    {

        public static void ConfigureNovinDbContext(this ModelBuilder builder)
        {

            //builder.Entity<Customer>(b =>
            //{
            //    b.HasIndex(b => b.Id);
            //    b.Property(b => b.CusId).IsRequired().ValueGeneratedOnAdd().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            //    b.Property(r => r.Name).HasMaxLength(50).IsRequired();
            //    b.Property(r => r.NationalCode).HasMaxLength(12).IsRequired();
            //    b.Property(r => r.Mobile).HasMaxLength(20).IsRequired();
            //    b.Property(r => r.Address).HasMaxLength(150);
            //    b.Property(r => r.IsActive).HasDefaultValue(true);
            //});

            builder.Entity<Movie>(b =>
            {
                b.HasIndex(t => t.Id);
                b.Property(t => t.Name).IsRequired().HasMaxLength(50);
                b.Property(t => t.DirectorName).IsRequired().HasMaxLength(50);

                b.HasOne(t => t.Categury)
                 .WithMany(t => t.Movies)
                 .HasForeignKey(t => t.CateguryId)
                 .OnDelete(DeleteBehavior.Cascade); ;
                
                b.HasOne(t => t.Genre)
                 .WithMany(t => t.Movies)
                 .HasForeignKey(t => t.GenreId)
                 .OnDelete(DeleteBehavior.Cascade); ;
            });
            
            
            builder.Entity<Categury>(b =>
            {
                b.HasIndex(t => t.Id);
                b.Property(t => t.Name).IsRequired();
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
