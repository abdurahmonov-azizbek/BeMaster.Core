// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by Abdurahmonov-azizbek
// --------------------------------------------------------

using BeMaster.Domain.Common.Exceptions;
using BeMaster.Domain.Entities;
using BeMaster.Persistence.DbContexts;
using BeMaster.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace BeMaster.Persistence.Repositories
{
    public class UserRepository(AppDbContext dbContext)
        : EntityRepositoryBase<User>(dbContext), IUserRepository
    {
        public new ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default) =>
                base.CreateAsync(user, saveChanges, cancellationToken);

        public new ValueTask<User> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default) =>
            base.DeleteByIdAsync(id, saveChanges, cancellationToken);

        public new IQueryable<User> Get(Expression<Func<User, bool>>? predicate = null, bool asNoTracking = false) =>
            base.Get(predicate, asNoTracking);

        public new ValueTask<User?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default) =>
            base.GetByIdAsync(id, asNoTracking, cancellationToken);

        public new ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var foundedUser = dbContext.Users.FirstOrDefault(dbUser =>
                dbUser.Id == user.Id)
                    ?? throw new EntityNotFoundException(typeof(User));

            foundedUser.FirstName = user.FirstName;
            foundedUser.LastName = user.LastName;
            foundedUser.PhoneNumber = user.PhoneNumber;
            foundedUser.Level = user.Level;

            return base.UpdateAsync(foundedUser, saveChanges, cancellationToken);
        }
    }
}