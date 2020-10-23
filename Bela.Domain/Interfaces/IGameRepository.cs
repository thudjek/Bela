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
        Task<Game> GetGameWithRoundsById(int gameId);
        Task<Round> GetRoundById(int roundId);
    }
}
