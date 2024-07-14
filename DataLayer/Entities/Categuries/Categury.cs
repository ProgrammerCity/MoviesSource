using Domain.Abstractions;
using Domain.Models.Movies;
using DomainShared.Error;

namespace Domain.Models.Catequries
{
    public class Categury : Entity<Guid>
    {
        #region Navigation
        public List<Movie> Movies { get; private set; } = [];
        #endregion

        #region Properties
        public string Name { get; set; } = default!;
        #endregion

        #region ctor
        public Categury()
        {

        }

        public Categury(Guid id,
            string name)
        {
            Id = id;
            SetName(name);
        }
        #endregion

        #region Method
        public Categury SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(CoreError.IsMandatory("عنوان"));
            }

            if (name.Length > 50)
            {
                throw new ArgumentException(CoreError.IsLess("عنوان", "پنجاه"));
            }

            Name = name;
            return this;
        }
        #endregion
    }
}
