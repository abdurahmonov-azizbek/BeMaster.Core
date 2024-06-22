// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by Abdurahmonov-azizbek
// --------------------------------------------------------

using BeMaster.Domain.Common.Filters;
using BeMaster.Domain.Entities;
using System.Linq.Expressions;

namespace BeMaster.Application.Common.Identity
{
    public interface IUserService
    {
        IQueryable<User> Get(SearchOptions searchOptions, bool asNoTracking = false);

        ValueTask<User?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);

        ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

        ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

        ValueTask<User> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);
    }
}
