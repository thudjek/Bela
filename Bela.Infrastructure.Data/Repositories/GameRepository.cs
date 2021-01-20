using Bela.Domain.Entities;
using Bela.Domain.Enums;
using Bela.Domain.Extensions;
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

        public async Task<Game> GetGameWithPlayersById(int gameId)
        {
            var game = await _dbContext.Games
                    .Include(g => g.PlayerGames).ThenInclude(pg => pg.Player)
                    .Where(g => g.Id == gameId && g.GameStatus == GameStatus.Playing)
                    .FirstOrDefaultAsync();

            return game;
        }

        public async Task<Game> GetGameWithRoundsById(int gameId)
        {
            var game = await _dbContext.Games
                .Include(g => g.Rounds).ThenInclude(r => r.CardsPlayed)
                .FirstOrDefaultAsync(g => g.Id == gameId);

            return game;
        }

        public Game GetGameWithPlayersAndRoundsById(int gameId)
        {
            var game = _dbContext.Games
                .Include(g => g.PlayerGames).ThenInclude(pg => pg.Player)
                .Include(g => g.Rounds)
                .FirstOrDefault(g => g.Id == gameId);

            return game;
        }

        public async Task<Round> GetRoundById(int roundId)
        {
            var round = await _dbContext.Rounds
                .Include(r => r.Calls)
                .Include(r => r.CardsPlayed)
                .FirstOrDefaultAsync(r => r.Id == roundId);
            return round;
        }

        public bool SaveGameDataForTimerElapsed(int gameId, ref string quitUsername, ref string opponent1Username, ref string opponent2Username)
        {
            BelaDbContextFactory factory = new BelaDbContextFactory();
            var dbCtx = factory.CreateDbContext(new string[] { });

            var game = dbCtx.Games
                .Include(g => g.PlayerGames).ThenInclude(pg => pg.Player)
                .Include(g => g.Rounds)
                .FirstOrDefault(g => g.Id == gameId);

            var lastRound = game.Rounds.OrderByDescending(r => r.RoundNumber).First();
            var currentPosition = lastRound.CurrentPlayerToPlay;
            quitUsername = game.PlayerGames.Where(pg => pg.Player.PlayerPosition == currentPosition).Select(pg => pg.Player).FirstOrDefault().UserName;
            opponent1Username = game.PlayerGames.Where(pg => pg.Player.PlayerPosition == currentPosition.GetNextPosition()).Select(pg => pg.Player).FirstOrDefault().UserName;
            opponent2Username = game.PlayerGames.Where(pg => pg.Player.PlayerPosition == currentPosition.GetPreviousPosition()).Select(pg => pg.Player).FirstOrDefault().UserName;

            var username = quitUsername;

            var quitPlayer = dbCtx.Players.FirstOrDefault(p => p.UserName == username);
            var query = game.PlayerGames.Select(pg => pg.Player);
            List<Player> players = quitPlayer.Team.Value == Team.FirstTeam ? query.OrderByDescending(p => p.Team).ToList() :
                     quitPlayer.Team.Value == Team.SecondTeam ? query.OrderBy(p => p.Team).ToList() : query.ToList();

            int counter = 0;
            foreach (var player in players)
            {
                counter++;
                var user = dbCtx.Users
                    .FirstOrDefault(u => u.Id == player.UserId);

                user.IsReady = false;
                user.UserStatus = UserStatus.InRoom;

                if (counter > 2)
                {
                    user.Losses++;
                    if (user.UserName == quitUsername)
                        user.Dropouts++;
                }
                else
                    user.Wins++;
            }

            var room = dbCtx.Rooms
                .FirstOrDefault(r => r.Id == game.RoomId);
            room.InGame = false;
            game.GameStatus = GameStatus.Ended;

            return dbCtx.SaveChanges() > 0;
        }
    }
}
