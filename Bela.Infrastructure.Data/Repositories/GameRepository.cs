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
    public class GameRepository : BaseRepository, IGameRepository
    {
        public GameRepository(BelaDbContext dbContext) : base(dbContext)
        {
            
        }

        public async Task<bool> IsRoomInGame(int roomId)
        {
            var game = await _dbContext.Games
                    .Where(g => g.RoomId == roomId && g.GameStatus == GameStatus.Playing)
                    .FirstOrDefaultAsync();

            return game != null;
        }

        public void CreateGame(Game game)
        {
            if (game == null)
                throw new ArgumentNullException(nameof(game));

            _dbContext.Games.Add(game);
        }

        public async Task<Game> GetGameById(int gameId)
        {
            var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == gameId);
            return game;
        }

        public async Task<Game> GetGameWithPlayersByRoomId(int roomId)
        {
            var game = await _dbContext.Games
                    .Include(g => g.PlayerGames).ThenInclude(pg => pg.Player)
                    .Where(g => g.RoomId == roomId && g.GameStatus == GameStatus.Playing)
                    .FirstOrDefaultAsync();

            return game;
        }

        public async Task<Game> GetGameWithRoundsById(int gameId)
        {
            var game = await _dbContext.Games
                .Include(g => g.Rounds).ThenInclude(r => r.GameActions)
                .FirstOrDefaultAsync(g => g.Id == gameId);

            return game;
        }

        public async Task<Round> GetRoundById(int roundId)
        {
            var round = await _dbContext.Rounds.FirstOrDefaultAsync(r => r.Id == roundId);
            return round;
        }
    }
}
