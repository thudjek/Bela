using Bela.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bela.Domain.Interfaces
{
    public interface IGameRepository : IBaseRepository
    {
        Task<bool> IsRoomInGame(int roomId);
        void CreateGame(Game game);
        Task<Game> GetGameById(int gameId);
        Task<Game> GetGameWithPlayersByRoomId(int roomId);
        Task<Game> GetGameWithPlayersById(int gameId);
        Task<Game> GetGameWithRoundsById(int gameId);
        Game GetGameWithPlayersAndRoundsById(int gameId);
        Task<Round> GetRoundById(int roundId);
        bool SaveGameDataForTimerElapsed(int gameId, ref string quitUsername, ref string opponent1Username, ref string opponent2Username);
    }
}
