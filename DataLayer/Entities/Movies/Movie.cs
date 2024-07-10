using Domain.Abstractions;
using Domain.Models.Catequries;
using Domain.Models.Genres;
using DomainShared.Error;

namespace Domain.Models.Movies
{
    public class Movie : AuditedEntity<Guid>
    {
        #region Navigatoin
        public List<Categury> Categuries{ get; } = [];

        public List<Genre> Genres{ get; } = [];
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
            Guid categuriesId,
            string name,
            Guid genre,
            float rate,
            int constructionYear,
            string directorName)
        {
            Id = id;
            //SetCateguries(categuriesId);
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
            //GenreId = genreId;
            return this;

        }

        public Movie SetCateguries(List<Categury> categuries)
        {
            //if (categuries.Count == 0)
            //{
            //    throw new ArgumentException("افزودن حداقل یک دسته الزامیست!!!");
            //}
            //MovieCategury.AddRange(categuries.Select(t=> new Movies.MovieCategury() { CateguryId = t.CreatorId}))
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
                throw new ArgumentException(CoreError.IsLess("نام فیلم", "پنجاه"));
            }
            Name = name;
            return this;
        }
    }
    #endregion

    //public class MovieGenre
    //{
    //    public Guid MovieId { get; set; }
    //    public Guid GenreId { get; set; }
    //    public Genre Genre { get; set; } = default!;
    //    public Movie Movie { get; set; } = default!;
    //}

    //public class MovieCategury
    //{
    //    public Guid MovieId { get; set; }
    //    public Guid CateguryId { get; set; }
    //    public Categury Categury { get; set; } = default!;
    //    public Movie Movie { get; set; } = default!;
    //}
}
