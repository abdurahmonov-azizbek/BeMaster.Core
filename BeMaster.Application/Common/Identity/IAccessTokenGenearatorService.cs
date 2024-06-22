// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by Abdurahmonov-azizbek
// --------------------------------------------------------

using BeMaster.Domain.Entities;

namespace BeMaster.Application.Common.Identity
{
    public interface IAccessTokenGeneratorService
    {
        string GetToken(User user);
    }
}
