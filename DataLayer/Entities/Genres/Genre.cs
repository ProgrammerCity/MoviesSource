using Domain.Abstractions;
using Domain.Models.Movies;
using DomainShared.Error;

namespace Domain.Models.Genres
{
    public class Genre : Entity<Guid>
    {
        #region Navigation
        public ICollection<Movie> Movies { get; set; } = [];
        #endregion

        #region Properties
        public string Titele { get; set; } = default!;
        #endregion

        #region ctor
        public Genre()
        {

        }

        public Genre(Guid id,
            string titele)
        {
            Id = id;
            SetTitele(titele);
        }
        #endregion

        #region Method
        public Genre SetTitele(string titele)
        {
            if (string.IsNullOrEmpty(titele))
            {
                throw new ArgumentException(CoreError.IsMandatory("عنوان"));
            }

            if (titele.Length > 50)
            {
                throw new ArgumentException(CoreError.IsLess("عنوان", "پنجاه"));
            }

            Titele = titele;
            return this;
        }
        #endregion
    }
}
