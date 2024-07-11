﻿using Domain.Abstractions;
using Domain.Entities.Actors;
using Domain.Models.Catequries;
using Domain.Models.Genres;
using DomainShared.Error;

namespace Domain.Models.Movies
{
    public class Movie : AuditedEntity<Guid>
    {
        #region Navigatoin
        public List<MovieCategory> MovieCategory { get; } = [];

        public List<Genre> Genres { get; } = [];

        public List<Actor> Actors { get; } = [];
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
            List<Categury> categuries,
            string name,
            List<Genre> genres,
            List<Actor> actor,
            float rate,
            int constructionYear,
            string directorName)
        {
            Id = id;
            SetCateguries(categuries);
            SetName(name);
            //SetGenre(genres);
            //SetActors(actor);
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

        public Movie SetGenre(List<Genre> genres)
        {
            if (genres.Count == 0)
            {
                throw new ArgumentException("افزودن حداقل یک ژانر الزامیست!!!");
            }
            Genres.Clear();
            Genres.AddRange(genres);
            return this;

        }
        
        public Movie SetActors(List<Actor> acts)
        {
            if (acts.Count == 0)
            {
                throw new ArgumentException("افزودن حداقل یک بازیگر الزامیست!!!");
            }
            Actors.Clear();
            Actors.AddRange(acts);
            return this;

        }

        public Movie SetCateguries(List<Categury> categuries)
        {
            if (categuries.Count == 0)
            {
                throw new ArgumentException("افزودن حداقل یک دسته الزامیست!!!");
            }
            Categuries.Clear();
            Categuries.AddRange(categuries);
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

    public class MovieCategory
    {
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
        public Guid CategoryId { get; set; }
        public Categury Category { get; set; }
    }
    #endregion
}
