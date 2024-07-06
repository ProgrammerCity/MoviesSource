using Domain.Abstractions;
using Domain.Models.Catequries;
using Domain.Models.Genres;
using DomainShared.Error;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Models.Movies
{
    public class Movie : AuditedEntity<Guid>
    {
        #region Navigatoin
        //public Categury Categury { get; private set; } = new();
        public Guid CateguryId { get; private set; }

        //public Genre Genre { get; private set; } = new();
        public Guid GenreId { get; private set; }
        #endregion

        #region Properties
        public string Name { get; private set; } = default!;
        public string DirectorName { get; private set; } = default!;
        public int ConstructionYear { get; private set; } = default!;
        public float Rate { get; private set; } = default!;
        #endregion

        #region Ctor
        internal Movie()
        {

        }

        public Movie(Guid id,
            Guid categuryId,
            string name,
            Guid genre,
            float rate,
            int constructionYear,
            string directorName)
        {
            Id = id;
            SetCategury(categuryId);
            SetName(name);
            SetGenre(genre);
            SetRate(rate);
            SetDirectorNam(directorName);
            SetCustructionYear(constructionYear);
        }

        #endregion

        #region Method
        public Movie SetCustructionYear(int constructionYear)
        {
            if (constructionYear < 0)
            {
                throw new ArgumentException(CoreError.IsMoreThan("سال ساخت", "صفر"));
            }
            ConstructionYear = constructionYear;
            return this;
        }

        public Movie SetDirectorNam(string directorName)
        {
            if (string.IsNullOrEmpty(directorName))
            {
                throw new ArgumentException(CoreError.IsMandatory("نام کارگردان"));
            }

            if (directorName.Length > 50)
            {
                throw new ArgumentException(CoreError.IsLess("نام کارگردان", "پنجاه"));
            }
            DirectorName = directorName;
            return this;
        }

        public Movie SetRate(float rate)
        {
            if (rate < 0)
            {
                throw new ArgumentException(CoreError.IsMoreThan("امتیاز", "صفر"));
            }
            Rate = rate;
            return this;
        }

        public Movie SetGenre(Guid genreId)
        {
            if (genreId == Guid.Empty)
            {
                throw new ArgumentException(CoreError.IsMandatory("شناسه ژانر"));
            }
            GenreId = genreId;
            return this;

        }

        public Movie SetCategury(Guid catequryId)
        {
            if (catequryId == Guid.Empty)
            {
                throw new ArgumentException(CoreError.IsMandatory("شناسه گروه"));
            }
            CateguryId = catequryId;
            return this;

        }

        public Movie SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(CoreError.IsMandatory("نام فیلم"));
            }

            if (name.Length > 50)
            {
                throw new ArgumentException(CoreError.IsLess("نام فیلم","پنجاه"));
            }
            Name = name;
            return this;
        }
    }
    #endregion

}
