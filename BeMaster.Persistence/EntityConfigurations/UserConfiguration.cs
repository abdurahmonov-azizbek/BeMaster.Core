// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by Abdurahmonov-azizbek
// --------------------------------------------------------

using BeMaster.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeMaster.Persistence.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(user => user.PhoneNumber).IsUnique();
        }
    }
}
