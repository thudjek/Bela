using Bela.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Infrastructure.Data.Configurations
{
    public class GameActionConfig : IEntityTypeConfiguration<GameAction>
    {
        public void Configure(EntityTypeBuilder<GameAction> builder)
        {
            //builder.HasOne(ga => ga.Round)
            //       .WithMany(r => r.GameActions)
            //       .HasForeignKey(ga => ga.RoundId);
        }
    }
}
