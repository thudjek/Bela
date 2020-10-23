using Bela.Application.Utility;
using Bela.Application.ViewModels.Game;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bela.Application.Interfaces
{
    public interface IGameService
    {
        Task<Result> StartGame(int roomId);
        Task<bool> IsPlayerInGame(int userId);
        Task<Result> IsRoomInGame(int roomId);
        Task<GameViewModel> GetGameViewModelAsync(int userId);
        Task<List<CardInHandModel>> GetCardHandModelListForPlayer(int userId, bool allCards);
        Task<CurrentGameDataViewModel> GetCurrentRoundData(int gameId, int userId);
        Task<Dictionary<string, int>> GetTotalScores(int gameId, int userId);
        Task<Dictionary<string, int>> GetRoundScores(int gameId, int userId);
        Task<Result> SelectTrump(int roundId, int trump, string username);
        Task<bool> MakeACall(List<string> cardStrings, int roundId, int userId);
    }
}
