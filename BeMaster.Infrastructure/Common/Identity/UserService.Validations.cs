// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by Abdurahmonov-azizbek
// --------------------------------------------------------

using BeMaster.Domain.Common.Exceptions;
using BeMaster.Domain.Entities;
using System.Data;

namespace BeMaster.Infrastructure.Common.Identity
{
    public partial class UserService
    {
        private void ValidateOnAdd(User user)
        {
            Validate(
                (Rule: IsInvalid(user.Id), Parameter: nameof(User.Id)),
                (Rule: IsInvalid(user.FirstName), Parameter: nameof(User.FirstName)),
                (Rule: IsInvalid(user.LastName), Parameter: nameof(User.LastName)),
                (Rule: IsInvalid(user.PhoneNumber), Parameter: nameof(User.PhoneNumber)));

            var users = userRepository.Get();
            if (users.Any(dbUser => dbUser.PhoneNumber == user.PhoneNumber))
            {
                throw new EntityValidationException("User is already exist with this phone number");
            }
        }

        private void ValidateOnUpdate(User user)
        {
            Validate(
                (Rule: IsInvalid(user.Id), Parameter: nameof(User.Id)),
                (Rule: IsInvalid(user.FirstName), Parameter: nameof(User.FirstName)),
                (Rule: IsInvalid(user.LastName), Parameter: nameof(User.LastName)),
                (Rule: IsInvalid(user.PhoneNumber), Parameter: nameof(User.PhoneNumber)));
        }

        private void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    throw new EntityValidationException($"{parameter} - required");
                }
            }
        }

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required."
        };

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == default,
            Message = "Id is required."
        };

        private static dynamic IsInvalid(DateTime date) => new
        {
            Condition = date == default,
            Message = "Date is required."
        };
    }
}
