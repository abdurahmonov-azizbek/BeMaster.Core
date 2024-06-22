// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by Abdurahmonov-azizbek
// --------------------------------------------------------

using BeMaster.Domain.Common.Exceptions;
using BeMaster.Domain.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BeMaster.Persistence.Repositories
{
    public class EntityRepositoryBase<TEntity>(DbContext dbContext)
        where TEntity : class, IEntity
    {
        protected async ValueTask<TEntity> CreateAsync(
            TEntity entity,
            bool saveChanges = true,
            CancellationToken cancellationToken = default)
        {
            await dbContext.AddAsync(entity, cancellationToken);

            if (saveChanges)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }

            return entity;
        }

        protected IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>>? predicate = default,
            bool asNoTracking = false)
        {
            var initialQuery = dbContext.Set<TEntity>().AsQueryable();

            if (predicate is not null)
            {
                initialQuery = initialQuery.Where(predicate);
            }

            if (asNoTracking)
            {
                initialQuery.AsNoTracking();
            }

            return initialQuery;
        }

        protected async ValueTask<TEntity?> GetByIdAsync(
            Guid id,
            bool asNoTracking = false,
            CancellationToken cancellationToken = default)
        {
            var initialQuery = dbContext.Set<TEntity>().AsQueryable();

            if (asNoTracking)
            {
                initialQuery = initialQuery.AsNoTracking();
            }

            var foundEntity = await initialQuery.FirstOrDefaultAsync(entity =>
                entity.Id == id, cancellationToken: cancellationToken)
                    ?? throw new EntityNotFoundException(typeof(TEntity));

            return foundEntity;
        }

        protected async ValueTask<TEntity> UpdateAsync(
            TEntity entity,
            bool saveChanges = true,
            CancellationToken cancellationToken = default)
        {
            dbContext.Update(entity);

            if (saveChanges)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }

            return entity;
        }

        protected async ValueTask<TEntity> DeleteByIdAsync(
            Guid id, 
            bool saveChanges = true, 
            CancellationToken cancellationToken = default)
        {
            var entity = await dbContext.Set<TEntity>().FirstOrDefaultAsync(dbEntity => dbEntity.Id == id)
                ?? throw new EntityNotFoundException(typeof(TEntity));

            dbContext.Remove(entity);

            if (saveChanges)
                await dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
