using Bela.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Infrastructure.Data.Configurations
{
    public class PlayerConfig : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            //builder.HasOne(p => p.User)
            //       .WithOne(u => u.Player)
            //       .HasForeignKey<Player>(p => p.UserId)
            //       .IsRequired();
        }
    }
}
