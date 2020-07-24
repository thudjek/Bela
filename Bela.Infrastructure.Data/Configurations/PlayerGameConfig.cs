using Bela.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Infrastructure.Data.Configurations
{
    public class PlayerGameConfig : IEntityTypeConfiguration<PlayerGame>
    {
        public void Configure(EntityTypeBuilder<PlayerGame> builder)
        {
            //builder.HasOne(pg => pg.Player)
            //       .WithMany(p => p.PlayerGames)
            //       .HasForeignKey(pg => pg.PlayerId);

            //builder.HasOne(pg => pg.Game)
            //       .WithMany(p => p.PlayerGames)
            //       .HasForeignKey(pg => pg.GameId);

        }
    }
}
