using Domain.Abstractions;
using Domain.Models.Movies;

namespace Domain.Entities.Actors
{
    public class Actor : AuditedEntity<Guid>
    {
        #region Navigatoin
        public List<Movie> Movies { get; } = [];
        #endregion

        #region Properties
        /// <summary>
        /// real name
        /// </summary>
        public string Name { get; private set; } = default!;
        /// <summary>
        /// most 3 famous nickname
        /// </summary>
        public string Nickname { get; private set; } = default!;

        /// <summary>
        /// address
        /// </summary>
        public string Path { get; private set; } = default!;
        #endregion

        #region Ctor
        internal Actor()
        {

        }
        #endregion
    }
}

