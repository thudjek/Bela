using Bela.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Infrastructure.Data.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(u => u.Room)
                   .WithMany(r => r.Users)
                   .HasForeignKey(u => u.RoomId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
