using Bela.Application.Interfaces;
using Bela.Application.Utility;
using Bela.Domain.Entities;
using Bela.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bela.Domain.Enums;
using System.Diagnostics.Contracts;
using Bela.Application.ViewModels.Game;
using System.Security.Cryptography;
using Bela.Domain.Extensions;
using System.Data;

namespace Bela.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IIdentityService _identityService;
        private readonly IRoomRepository _roomRepository;
        private readonly IGameRepository _gameRepository;
        public GameService(
            IPlayerRepository playerRepository,
            IIdentityService identityService,
            IRoomRepository roomRepository,
            IGameRepository gameRepository)
        {
            _playerRepository = playerRepository;
            _identityService = identityService;
            _roomRepository = roomRepository;
            _gameRepository = gameRepository;
        }

        public async Task<Result> StartGame(int roomId)
        {
            Room room = await _roomRepository.GetByIdWithUsersAsync(roomId);
            var game = new Game()
            {
                GameStatus = GameStatus.Playing,
                RoomId = roomId
            };

            _gameRepository.CreateGame(game);

            var players = await _playerRepository.GetPlayerListByIds(room.Users.Select(u => u.Id).ToList());

            FillPlayersDataAndAddToGame(room.Users, players, game);
            FillPlayersHands(players);
            AddFirstRound(game);

            var result = await _gameRepository.SaveAsync();

            if (!result)
                return Result.Fail(new string[] { "Dogodila se greška" });

            return Result.Success();
        }

        public async Task<bool> IsPlayerInGame(int userId)
        {
            return await _playerRepository.IsPlayerInGame(userId);
        }

        public async Task<Result> IsRoomInGame(int roomId)
        {
            var isRoomInGame = await _gameRepository.IsRoomInGame(roomId);
            if (isRoomInGame)
            {
                return Result.Success();
            }
            else
            {
                return Result.Fail(new string[] { "Dogodila se greška" });
            }
        }

        public async Task<GameViewModel> GetGameViewModelAsync(int userId)
        {
            var roomId = await _identityService.GetUsersRoomId(userId);
            var game = await _gameRepository.GetGameWithPlayersByRoomId(roomId);
            var players = game.PlayerGames.Select(pg => pg.Player).ToList();
            var currentPlayer = players.FirstOrDefault(p => p.UserId == userId);
            var model = new GameViewModel()
            {
                Id = game.Id,
                Position = currentPlayer.PlayerPosition.Value,
                UserNameDown = currentPlayer.UserName,
                UserNameLeft = GetUsernameByPosition(players, currentPlayer.PlayerPosition.Value.GetPreviousPosition()),
                UserNameRight = GetUsernameByPosition(players, currentPlayer.PlayerPosition.Value.GetNextPosition()),
                UserNameUp = GetUsernameByPosition(players, currentPlayer.PlayerPosition.Value.GetTeammatePosition())
            };
            return model;
        }

        public async Task<List<CardInHandModel>> GetCardHandModelListForPlayer(int userId, bool allCards)
        {
            List<CardInHandModel> listOfCards = new List<CardInHandModel>();
            var player = await _playerRepository.GetPlayerByUserIdAsync(userId);
            var cards = CardsManager.GetCardListFromHandString(player.Hand);
            if (allCards)
            {
                cards = cards.OrderCards();
                listOfCards = cards
                    .Select(c => new CardInHandModel()
                    {
                        CardString = c.ToString(),
                        CardUrl = c.ImgPath
                    }).ToList();
            }
            else
            {
                var firstSix = cards.Take(6).ToList().OrderCards();
                listOfCards = firstSix
                    .Select(c => new CardInHandModel() 
                    {
                        CardString = c.ToString(),
                        CardUrl = c.ImgPath
                    }).ToList();
                listOfCards.Add(new CardInHandModel() { CardString = "", CardUrl = CardsManager.GetBackgroundCardImgPath() });
                listOfCards.Add(new CardInHandModel() { CardString = "", CardUrl = CardsManager.GetBackgroundCardImgPath() });
            }

            return listOfCards;
        }

        public async Task<CurrentGameDataViewModel> GetCurrentRoundData(int gameId, int userId)
        {
            var model = new CurrentGameDataViewModel();
            var game = await _gameRepository.GetGameWithRoundsById(gameId);
            var player = await _playerRepository.GetPlayerByUserIdAsync(userId);
            var round = game.Rounds.OrderByDescending(r => r.RoundNumber).FirstOrDefault();

            FillBasicRoundData(model, round, player.PlayerPosition.Value);
            FillTotalScores(game, player.Team.Value, model: model);
            FillRoundScores(round, player.Team.Value, model: model);

            if ((int)round.RoundPhase > 2)
            {
                var gameActions = round.GameActions.Where(ga => ga.RoundPhase == round.RoundPhase).OrderBy(ga => ga.Id).ToList();
                FillPlayedCards(model, gameActions, player.PlayerPosition.Value);
            }
            return model;
        }

        public async Task<Dictionary<string, int>> GetTotalScores(int gameId, int userId)
        {
            var game = await _gameRepository.GetGameById(gameId);
            var player = await _playerRepository.GetPlayerByUserIdAsync(userId);
            Dictionary<string, int> scores = new Dictionary<string, int>();
            FillTotalScores(game, player.Team.Value, scores: scores);
            return scores;
        }

        public async Task<Dictionary<string, int>> GetRoundScores(int roundId, int userId)
        {
            var round = await _gameRepository.GetRoundById(roundId);
            var player = await _playerRepository.GetPlayerByUserIdAsync(userId);
            Dictionary<string, int> scores = new Dictionary<string, int>();
            FillRoundScores(round, player.Team.Value, scores: scores);
            return scores;
        }

        public async Task<Result> SelectTrump(int roundId, int trump, string username)
        {
            var result = Result.Success();
            Round round = await _gameRepository.GetRoundById(roundId);
            if (trump > 0)
            {
                round.CurrentPlayerToPlay = round.FirstPlayerToPlay;
                round.RoundPhase = round.RoundPhase.GetNextPhase();
                round.CurrentTrump = (CardSuit)trump;
                round.TrumpSelectedBy = username;
                result.Values = new object[] 
                {
                    round.GameId.ToString(),
                    new 
                    {
                        roundPhase = (int)round.RoundPhase,
                        positionToPlayUp = (int)PlayerPosition.Up.GetOnScreenPosition(round.CurrentPlayerToPlay),
                        positionToPlayDown = (int)PlayerPosition.Down.GetOnScreenPosition(round.CurrentPlayerToPlay),
                        positionToPlayLeft = (int)PlayerPosition.Left.GetOnScreenPosition(round.CurrentPlayerToPlay),
                        positionToPlayRight = (int)PlayerPosition.Right.GetOnScreenPosition(round.CurrentPlayerToPlay),
                        selectedTrump = (int)round.CurrentTrump,
                        trumpSelectedBy = round.TrumpSelectedBy
                    } 
                };
            }
            else
            {
                round.CurrentPlayerToPlay = round.CurrentPlayerToPlay.GetNextPosition();
                result.Values = new object[]
                {
                    round.GameId.ToString() ,
                    new {
                        positionToPlayUp = (int)PlayerPosition.Up.GetOnScreenPosition(round.CurrentPlayerToPlay),
                        positionToPlayDown = (int)PlayerPosition.Down.GetOnScreenPosition(round.CurrentPlayerToPlay),
                        positionToPlayLeft = (int)PlayerPosition.Left.GetOnScreenPosition(round.CurrentPlayerToPlay),
                        positionToPlayRight = (int)PlayerPosition.Right.GetOnScreenPosition(round.CurrentPlayerToPlay),
                        isLast = round.CurrentPlayerToPlay == round.FirstPlayerToPlay.GetPreviousPosition()
                    } 
                };
            }

            if (await _gameRepository.SaveAsync())
                return result;

            return Result.Fail(new string[] { "Dogodila se greška" });
        }

        public async Task<bool> MakeACall(List<string> cardStrings, int roundId, int userId)
        {
            Round round = null;
            Player player = null;
            List<Card> cards = cardStrings.Select(cs => CardsManager.GetCardFromString(cs)).ToList();
            if (cardStrings.Count > 2)
            {
                if (IsFourOfAKind(cards))
                {
                    round = await _gameRepository.GetRoundById(roundId);
                    player = await _playerRepository.GetPlayerByUserIdAsync(userId);
                    round.GameActions.Add(new GameAction()
                    {
                        RoundPhase = round.RoundPhase,
                        PlayerPosition = player.PlayerPosition.Value,
                        Call = Call.FourOfAKind,
                        HighestValueInACall = cards.OrderByDescending(c => c.Value).Select(c => c.Value).FirstOrDefault()
                    });
                }
                else if (AreAllCardsSameSuit(cards) && AreCardsInSequence(cards))
                {
                    round = await _gameRepository.GetRoundById(roundId);
                    player = await _playerRepository.GetPlayerByUserIdAsync(userId);
                    round.GameActions.Add(new GameAction()
                    {
                        RoundPhase = round.RoundPhase,
                        PlayerPosition = player.PlayerPosition.Value,
                        Call = (Call)(cards.Count-2),
                        HighestValueInACall = cards.OrderByDescending(c => c.Value).Select(c => c.Value).FirstOrDefault()
                    });
                }
            }
            
            return false;
        }

        private string GetUsernameByPosition(List<Player> players, PlayerPosition position)
        {
            return players.FirstOrDefault(p => p.PlayerPosition == position).UserName;
        }

        private void FillPlayersDataAndAddToGame(List<User> users, List<Player> players, Game game)
        {
            foreach (var player in players)
            {
                var user = users.FirstOrDefault(u => u.Id == player.Id);
                player.Team = user.RoomOrderNumber.Value == 1 || user.RoomOrderNumber.Value == 2 ? Team.FirstTeam : Team.SecondTeam;

                player.PlayerPosition = user.RoomOrderNumber.Value == 1 ? PlayerPosition.Down :
                                        user.RoomOrderNumber.Value == 2 ? PlayerPosition.Up :
                                        user.RoomOrderNumber.Value == 3 ? PlayerPosition.Right :
                                                                            PlayerPosition.Left;
            }

            players.ForEach(p => game.PlayerGames.Add(new PlayerGame() { PlayerId = p.Id }));
        }

        private void AddFirstRound(Game game)
        {
            Random rnd = new Random();
            int randomPosition = rnd.Next(1, 5);
            Round round = new Round()
            {
                RoundNumber = 1,
                RoundPhase = RoundPhase.TrumpCalling,
                FirstPlayerToPlay = (PlayerPosition)randomPosition,
                CurrentPlayerToPlay = (PlayerPosition)randomPosition
            };
            game.Rounds.Add(round);
        }

        private void FillPlayersHands(List<Player> players)
        {
            var deck = CardsManager.GetShuffledDeck();
            players.ForEach(p => p.Hand = deck.DealCards(8));
        }

        private void FillTotalScores(Game game, Team team, Dictionary<string, int> scores = null, CurrentGameDataViewModel model = null)
        {
            switch (team)
            {
                case Team.FirstTeam:
                    if (scores != null)
                    {
                        scores.Add("miTotalScore", game.FirstTeamTotalScore);
                        scores.Add("viTotalScore", game.SecondTeamTotalScore);
                    }
                    else if (model != null)
                    {
                        model.MiTotalScore = game.FirstTeamTotalScore;
                        model.ViTotalScore = game.SecondTeamTotalScore;
                    }
                    break;
                case Team.SecondTeam:
                    if (scores != null)
                    {
                        scores.Add("miTotalScore", game.SecondTeamTotalScore);
                        scores.Add("viTotalScore", game.FirstTeamTotalScore);
                    }
                    else if (model != null)
                    {
                        model.MiTotalScore = game.SecondTeamTotalScore;
                        model.ViTotalScore = game.FirstTeamTotalScore;
                    }
                    break;
            }
        }

        private void FillRoundScores(Round round, Team team, Dictionary<string, int> scores = null, CurrentGameDataViewModel model = null)
        {
            switch (team)
            {
                case Team.FirstTeam:
                    if (scores != null)
                    {
                        scores.Add("miCalls", round.FirstTeamCalls);
                        scores.Add("viCalls", round.SecondTeamCalls);
                        scores.Add("miPoints", round.FirstTeamScore);
                        scores.Add("viPoints", round.SecondTeamScore);
                        scores.Add("miRoundTotal", round.FirstTeamRoundTotal);
                        scores.Add("viRoundTotal", round.SecondTeamRoundTotal);
                    }
                    else if (model != null)
                    {
                        model.MiCalls = round.FirstTeamCalls;
                        model.ViCalls = round.SecondTeamCalls;
                        model.MiPoints = round.FirstTeamScore;
                        model.ViPoints = round.SecondTeamScore;
                        model.MiRoundTotal = round.FirstTeamRoundTotal;
                        model.ViRoundTotal = round.SecondTeamRoundTotal;
                    }
                    break;
                case Team.SecondTeam:
                    if (scores != null)
                    {
                        scores.Add("miCalls", round.SecondTeamCalls);
                        scores.Add("viCalls", round.FirstTeamCalls);
                        scores.Add("miPoints", round.SecondTeamScore);
                        scores.Add("viPoints", round.FirstTeamScore);
                        scores.Add("miRoundTotal", round.SecondTeamRoundTotal);
                        scores.Add("viRoundTotal", round.FirstTeamRoundTotal);
                    }
                    else if (model != null)
                    {
                        model.MiCalls = round.SecondTeamCalls;
                        model.ViCalls = round.FirstTeamCalls;
                        model.MiPoints = round.SecondTeamScore;
                        model.ViPoints = round.FirstTeamScore;
                        model.MiRoundTotal = round.SecondTeamRoundTotal;
                        model.ViRoundTotal = round.FirstTeamRoundTotal;
                    }
                    break;
            }
        }

        private void FillBasicRoundData(CurrentGameDataViewModel model, Round round, PlayerPosition playerPosition)
        {
            model.CurrentRoundId = round.Id;
            model.CurrentRoundPhase = (int)round.RoundPhase;
            model.SelectedTrump = round.CurrentTrump.HasValue ? (int)round.CurrentTrump : 0;
            model.TrumpSelectedBy = !string.IsNullOrEmpty(round.TrumpSelectedBy) ? round.TrumpSelectedBy : "";
            model.PositionToPlay = (int)playerPosition.GetOnScreenPosition(round.CurrentPlayerToPlay);
            model.DealerPosition = (int)playerPosition.GetOnScreenPosition(round.FirstPlayerToPlay.GetPreviousPosition());
            model.isLast = playerPosition == round.FirstPlayerToPlay.GetPreviousPosition();
        }

        private void FillPlayedCards(CurrentGameDataViewModel model, List<GameAction> gameActions, PlayerPosition playerPosition)
        {
            foreach (var gameAction in gameActions)
            {
                var onScreenPosition = playerPosition.GetOnScreenPosition(gameAction.PlayerPosition);
                Card card = CardsManager.GetCardFromString(gameAction.CardPlayed);
                switch (onScreenPosition)
                {
                    case PlayerPosition.Up:
                        model.UpCard = card.ImgPath;
                            break;
                    case PlayerPosition.Left:
                        model.LeftCard = card.ImgPath;
                        break;
                    case PlayerPosition.Right:
                        model.RightCard = card.ImgPath;
                        break;
                    case PlayerPosition.Down:
                        model.DownCard = card.ImgPath;
                        break;
                }
            }
        }

        private bool IsFourOfAKind(List<Card> cards)
        {
            if (cards.Count == 4)
            {
                return cards.Select(c => c.Value).Distinct().ToList().Count == 1;
            }

            return false;
        }

        private bool AreAllCardsSameSuit(List<Card> cards)
        {
            return cards.Select(c => c.Suit).Distinct().ToList().Count == 1;
        }

        private bool AreCardsInSequence(List<Card> cards)
        {
            var values = new List<int>();
            var counter = 0;
            foreach (var value in cards.Select(c => (int)c.Value).ToList())
            {
                values.Add(value - counter);
                counter++;
            }

            if (values.Distinct().Count() == 1)
                return true;
            else
                return false;
        }
    }
}
