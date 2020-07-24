using Bela.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Infrastructure.Data.Configurations
{
    public class RoundConfig : IEntityTypeConfiguration<Round>
    {
        public void Configure(EntityTypeBuilder<Round> builder)
        {
            //builder.HasOne(r => r.Game)
            //       .WithMany(g => g.Rounds)
            //       .HasForeignKey(r => r.GameId);
        }
    }
}
