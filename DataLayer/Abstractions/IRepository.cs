﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Domain.Abstractions
{
    public interface IRepository<TEntity> where TEntity : class, IEntities
    {
        DbSet<TEntity> Entities { get; }
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }

        void Add(TEntity entity, bool saveNow = true);
        Task<bool> AddAsync(TEntity entity, bool saveNow = true);
        void AddRange(IEnumerable<TEntity> entities, bool saveNow = true);
        Task<bool> AddRangeAsync(IEnumerable<TEntity> entities, bool saveNow = true);
        void Attach(TEntity entity);
        void Delete(TEntity entity, bool saveNow = true);
        Task<bool> DeleteAsync(TEntity entity, bool saveNow = true);
        Task<bool> DeleteAsync<T>(T entityId, bool saveNow = true);
        void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities, bool saveNow = true);
        void Detach(TEntity entity);
        TEntity GetById(params object[] ids);
        Task<TEntity> GetByIdAsync(params object[] ids);
        Task<TEntity> FindEntity(Expression<Func<TEntity, bool>> expression);
        List<TEntity> Tolist(Expression<Func<TEntity, bool>> expression = null);
        Task<List<TEntity>> TolistAsync(Expression<Func<TEntity, bool>> WhereExpression = null);
        Task<List<TEntity>> TolistAsync<TResult>(Expression<Func<TEntity, TResult>> selectExpression, Expression<Func<TEntity, bool>> expression = null);
        Task<List<TEntity>> TolistAsync<TProperty, TResult>(Expression<Func<TEntity, int, TResult>> selectExpression, Expression<Func<TEntity, TProperty>> IncludeExpression, Expression<Func<TEntity, bool>> expression = null);
        void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty) where TProperty : class;
        Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken) where TProperty : class;
        void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty) where TProperty : class;
        Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty, CancellationToken cancellationToken) where TProperty : class;
        void Update(TEntity entity, bool saveNow = true);
        Task<bool> UpdateAsync(TEntity entity, bool saveNow = true);
        void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true);
        Task<(bool isSuccess, string ErrorMessage)> UpdateRangeAsync(IEnumerable<TEntity> entities, bool saveNow = true);
    }
}

