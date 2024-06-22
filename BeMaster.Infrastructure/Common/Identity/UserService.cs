// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by Abdurahmonov-azizbek
// --------------------------------------------------------

using BeMaster.Application.Common.Identity;
using BeMaster.Domain.Common.Extensions;
using BeMaster.Domain.Common.Filters;
using BeMaster.Domain.Entities;
using BeMaster.Persistence.Repositories.Interfaces;

namespace BeMaster.Infrastructure.Common.Identity
{
    public partial class UserService(IUserRepository userRepository) : IUserService
    {
        public ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            ValidateOnAdd(user);

            return userRepository.CreateAsync(user, saveChanges, cancellationToken);
        }

        public ValueTask<User> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default) =>
            userRepository.DeleteByIdAsync(id, saveChanges, cancellationToken);

        public IQueryable<User> Get(SearchOptions searchOptions, bool asNoTracking = false)
        {
            var users = userRepository.Get();

            if (searchOptions.SearchKey is not null)
            {
                users = users.OrderBy(user => user.Id)
                    .Where(user => user.FirstName.ToLower().Contains(searchOptions.SearchKey.ToLower()) ||
                           user.LastName.ToLower().Contains(searchOptions.SearchKey.ToLower()) ||
                           user.PhoneNumber.ToLower().Contains(searchOptions.SearchKey.ToLower()));
            }
            else
            {
                users = users.OrderBy(user => user.Id);
            }

            return users.ApplyPagination(searchOptions);
        }

        public ValueTask<User?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default) =>
            userRepository.GetByIdAsync(id, asNoTracking, cancellationToken);

        public ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            ValidateOnUpdate(user);

            return userRepository.UpdateAsync(user, saveChanges, cancellationToken);
        }
    }
}
