using Bela.Domain.Entities;
using Bela.Domain.Interfaces;
using Bela.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bela.Infrastructure.Data.Repositories
{
    public class PlayerRepository : BaseRepository, IPlayerRepository
    {
        public PlayerRepository(BelaDbContext dbContext) : base(dbContext)
        {

        }

        public void CreatePlayer(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            _dbContext.Players.Add(player); 
        }
    }
}
