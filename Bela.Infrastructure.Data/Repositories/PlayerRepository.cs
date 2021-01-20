using Bela.Domain.Entities;
using Bela.Domain.Enums;
using Bela.Domain.Interfaces;
using Bela.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<bool> IsPlayerInGame(int playerId)
        {
            var playerGame = await _dbContext.PlayerGames
                .Include(pg => pg.Game)
                .Where(pg => pg.Game.GameStatus == GameStatus.Playing && pg.PlayerId == playerId)
                .FirstOrDefaultAsync();

            return playerGame != null;
        }

        public async Task<List<Player>> GetPlayerListByIds(List<int> playerIds)
        {
            return await _dbContext.Players.Where(p => playerIds.Contains(p.Id)).ToListAsync();
        }

        public async Task<Player> GetPlayerByUserIdAsync(int userId)
        {
            return await _dbContext.Players.FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<Player> GetPlayerByUsernameAsync(string username)
        {
            return await _dbContext.Players.FirstOrDefaultAsync(p => p.UserName == username);
        }

        public Player GetPlayerByUsername(string username)
        {
            return _dbContext.Players.FirstOrDefault(p => p.UserName == username);
        }
    }
}
