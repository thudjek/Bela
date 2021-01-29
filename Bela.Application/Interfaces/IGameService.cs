using Bela.Application.Utility;
using Bela.Application.ViewModels.Game;
using Bela.Domain.Enums;
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
        Task<EndScreenViewModel> GetEndScreenViewModel(int gameId, string myUsername, string teammateUsername, string opponent1Username, string opponent2Username, int userId);
        EndScreenViewModel GetEndScreenViewModelForPlayerQuitting(string quitUsername, string opponent1Username, string opponent2Username);
        Task<List<CardInHandModel>> GetCardHandModelListForPlayer(int userId, bool allCards, int trump);
        Task<List<List<string>>> GetListOfCardUrlsForCall(int roundId, int playerPosition, bool isPartner);
        Task<CurrentGameDataViewModel> GetCurrentRoundData(int gameId, int userId);
        Task<TotalScoresAndRoundsModel> GetTotalScoresAndRounds(int gameId, int userId);
        Task<Dictionary<string, int>> GetRoundScores(int gameId, int userId);
        Task<Result> SelectTrump(int roundId, int trump, string username);
        Task<Result> MakeACall(List<string> cardStrings, int roundId, int userId, bool isCall);
        Task<Result> PlayACard(string playedCardString, int roundId, int userId, PlayerPosition position, List<string> cardsInHandStrings, bool belaCalled);
        Task<Result> LeaveGame(int gameId, string quitUsername);
        Result LeaveGameTimerElapsed(int gameId, string connString);
    }
}
