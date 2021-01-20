using Bela.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bela.Domain.Interfaces
{
    public interface IPlayerRepository : IBaseRepository
    {
        void CreatePlayer(Player player);
        Task<bool> IsPlayerInGame(int playerId);
        Task<List<Player>> GetPlayerListByIds(List<int> playerIds);
        Task<Player> GetPlayerByUserIdAsync(int userId);
        Task<Player> GetPlayerByUsernameAsync(string username);
        Player GetPlayerByUsername(string username);
    }
}
