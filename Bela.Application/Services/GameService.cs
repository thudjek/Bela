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
using System.Collections.Specialized;
using System.Timers;

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
            room.InGame = true;
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

            var saveResult = await _gameRepository.SaveAsync();

            if (!saveResult)
                return Result.Fail(new string[] { "Dogodila se greška" });

            var result = Result.Success();
            result.Values = new object[] { game.Id };
            return result;
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

        public async Task<EndScreenViewModel> GetEndScreenViewModel(int gameId, string myUsername, string teammateUsername, string opponent1Username, string opponent2Username, int userId) 
        {
            var player = await _playerRepository.GetPlayerByUserIdAsync(userId);
            var game = await _gameRepository.GetGameById(gameId);
            var winningTeam = GetWinningTeamFromeScores(game);
            var myTeam = player.Team.Value;
            if (!winningTeam.HasValue)
                return null;

            var model = new EndScreenViewModel()
            {
                MyUsername = myUsername,
                TeammateUsername = teammateUsername,
                Opponent1Username = opponent1Username,
                Opponent2Username = opponent2Username,
                Winner1 = myTeam == winningTeam.Value ? myUsername : opponent1Username,
                Winner2 = myTeam == winningTeam.Value ? teammateUsername : opponent2Username,
                MyTeamScore = myTeam == Team.FirstTeam ? game.FirstTeamTotalScore : myTeam == Team.SecondTeam ? game.SecondTeamTotalScore : 0,
                OpponentTeamScore = myTeam == Team.FirstTeam ? game.SecondTeamTotalScore : myTeam == Team.SecondTeam ? game.FirstTeamTotalScore : 0,
                PlayerQuit = false,
                QuitUsername = ""
            };
            return model;
        }

        public EndScreenViewModel GetEndScreenViewModelForPlayerQuitting(string quitUsername, string opponent1Username, string opponent2Username)
        {
            var model = new EndScreenViewModel()
            {
                MyUsername = "",
                TeammateUsername = "",
                Opponent1Username = "",
                Opponent2Username = "",
                Winner1 = opponent1Username,
                Winner2 = opponent2Username,
                MyTeamScore = 0,
                OpponentTeamScore = 0,
                PlayerQuit = true,
                QuitUsername = quitUsername
            };
            return model;
        }

        private Team? GetWinningTeamFromeScores(Game game)
        {
            if (game.FirstTeamTotalScore > game.SecondTeamTotalScore)
                return Team.FirstTeam;

            if (game.SecondTeamTotalScore > game.FirstTeamTotalScore)
                return Team.SecondTeam;

            return null;
        }

        public async Task<List<CardInHandModel>> GetCardHandModelListForPlayer(int userId, bool allCards, int trump)
        {
            List<CardInHandModel> listOfCards = new List<CardInHandModel>();
            var player = await _playerRepository.GetPlayerByUserIdAsync(userId);

            if (string.IsNullOrEmpty(player.Hand))
                return listOfCards;

            var cards = CardsManager.GetCardListFromHandString(player.Hand);
            if (allCards)
            {
                cards = cards.OrderCards();
                var trumpSuit = (CardSuit)trump;
                var belaAvailable = cards.Exists(c => c.Value == CardValue.King && c.Suit == trumpSuit) && cards.Exists(c => c.Value == CardValue.Queen && c.Suit == trumpSuit);
                var trumpSuitString = trumpSuit.GetStringValue();
                listOfCards = cards
                    .Select(c => new CardInHandModel()
                    {
                        CardString = c.ToString(),
                        CardUrl = c.ImgPath,
                        AskForBela = belaAvailable && c.Suit == trumpSuit && (c.Value == CardValue.Queen || c.Value == CardValue.King)
                    }).ToList();
            }
            else
            {
                var firstSix = cards.Take(6).ToList().OrderCards();
                listOfCards = firstSix
                    .Select(c => new CardInHandModel() 
                    {
                        CardString = c.ToString(),
                        CardUrl = c.ImgPath,
                        AskForBela = false
                    }).ToList();
                listOfCards.Add(new CardInHandModel() { CardString = "", CardUrl = CardsManager.GetBackgroundCardImgPath() });
                listOfCards.Add(new CardInHandModel() { CardString = "", CardUrl = CardsManager.GetBackgroundCardImgPath() });
            }

            return listOfCards;
        }

        public async Task<List<List<string>>> GetListOfCardUrlsForCall(int roundId, int playerPosition, bool isPartner)
        {
            List<List<string>> urls = new List<List<string>>();
            var position = isPartner ? ((PlayerPosition)playerPosition).GetTeammatePosition() : (PlayerPosition)playerPosition;
            var round = await _gameRepository.GetRoundById(roundId);
            List<Call> calls = round.Calls.Where(c => c.PlayerPosition == position).ToList();
            foreach (var call in calls)
            {
                Card card = CardsManager.GetCardFromString(call.HighestCard);
                urls.Add(FindCallCardUrlsFromHighestCard(card, call.Type));
            }

            return urls;
        }

        public async Task<CurrentGameDataViewModel> GetCurrentRoundData(int gameId, int userId)
        {
            var model = new CurrentGameDataViewModel();
            var game = await _gameRepository.GetGameWithRoundsById(gameId);
            var player = await _playerRepository.GetPlayerByUserIdAsync(userId);
            Round round = game.Rounds.OrderByDescending(r => r.RoundNumber).FirstOrDefault();

            FillBasicRoundData(model, round, player.PlayerPosition.Value);
            FillTotalScores(game, player.Team.Value, model: model);
            FillRoundScores(round, player.Team.Value, model: model);
            FillCalls(model, round.Calls, player.PlayerPosition.Value);

            if ((int)round.RoundPhase > 2)
            {
                var cardsPlayed = round.CardsPlayed.Where(ga => ga.RoundPhase == round.RoundPhase).OrderBy(ga => ga.Id).ToList();
                FillPlayedCards(model, cardsPlayed, player.PlayerPosition.Value);
            }

            var rounds = game.Rounds.OrderBy(r => r.RoundNumber).ToList();
            AddRoundsToData(model.Rounds, rounds, player.Team.Value);

            return model;
        }

        public async Task<TotalScoresAndRoundsModel> GetTotalScoresAndRounds(int gameId, int userId)
        {
            var game = await _gameRepository.GetGameWithRoundsById(gameId);
            var player = await _playerRepository.GetPlayerByUserIdAsync(userId);
            TotalScoresAndRoundsModel model = new TotalScoresAndRoundsModel();
            FillTotalScores(game, player.Team.Value, scores: model.Scores);
            var rounds = game.Rounds.OrderBy(r => r.RoundNumber).ToList();
            AddRoundsToData(model.Rounds, rounds, player.Team.Value);
            return model;
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
            }
            else
            {
                round.CurrentPlayerToPlay = round.CurrentPlayerToPlay.GetNextPosition();
            }

            result.Values = GetResultObjForTrumpSelection(round);

            if (await _gameRepository.SaveAsync())
                return result;

            return Result.Fail(new string[] { "Dogodila se greška" });
        }

        public async Task<Result> MakeACall(List<string> cardStrings, int roundId, int userId, bool isCall)
        {
            var result = Result.Success();
            int highestCallValue = 0;
            PlayerPosition? winningCallPosition = null;
            Round round = await _gameRepository.GetRoundById(roundId);
            Player player = await _playerRepository.GetPlayerByUserIdAsync(userId);
            if (isCall)
            {
                if (cardStrings.Count > 2)
                {
                    List<Card> cards = cardStrings.Select(cs => CardsManager.GetCardFromString(cs)).ToList();
                    List<Call> foundCalls = new List<Call>();
                    List<Card> fourOfAKindRemained = new List<Card>();
                    List<Card> sequenceRemained = new List<Card>();
                    FindFourOfAKindCalls(cards, player.PlayerPosition.Value, ref foundCalls, ref fourOfAKindRemained);
                    FindSequenceCalls(cards, player.PlayerPosition.Value, ref foundCalls, ref sequenceRemained);
                    if (!DoListsHaveMatchingCard(fourOfAKindRemained, sequenceRemained))
                    {
                        round.Calls.AddRange(foundCalls);
                        highestCallValue = foundCalls.OrderByDescending(c => c.Value).FirstOrDefault().Value;
                    }
                    else
                    {
                        return Result.Fail(new string[] { "Nevažeće zvanje" });
                    }
                }
                else 
                {
                    return Result.Fail(new string[] { "Nevažeće zvanje" });
                }
            }

            round.CurrentPlayerToPlay = round.CurrentPlayerToPlay.GetNextPosition();
            if (round.CurrentPlayerToPlay == round.FirstPlayerToPlay)
            {
                round.RoundPhase = round.RoundPhase.GetNextPhase();
                if (round.Calls.Count > 0)
                {
                    winningCallPosition = GetWiningCallPosition(round);
                    await ResolveCalls(round, winningCallPosition.Value);
                }
                
            }
            result.Values = GetResultObjForCall(round, highestCallValue, winningCallPosition);

            if (await _gameRepository.SaveAsync())
                return result;

            return Result.Fail(new string[] { "Dogodila se greška" });
        }

        public async Task<Result> PlayACard(string playedCardString, int roundId, int userId, PlayerPosition position, List<string> cardsInHandStrings, bool belaCalled)
        {
            Round round = await _gameRepository.GetRoundById(roundId);
            Player player = await _playerRepository.GetPlayerByUserIdAsync(userId);

            var cardsPlayed = round.CardsPlayed.Where(cp => cp.RoundPhase == round.RoundPhase)
                            .OrderBy(cp => cp.DateCreated)
                            .Select(cp => CardsManager.GetCardFromString(cp.CardString))
                            .ToList();

            var cardsInHand = CardsManager.GetCardListFromHandString(string.Join(",", cardsInHandStrings));
            var playedCard = CardsManager.GetCardFromString(playedCardString);
            var validCards = ValidCardsHelper.GetValidCardsForPlay(cardsInHand, round.CurrentTrump.Value, cardsPlayed);

            if (validCards.Contains(playedCard))
            {
                var result = Result.Success();
                round.CardsPlayed.Add(new CardPlayed(round.RoundPhase, position, playedCard, round.CurrentTrump.Value));
                cardsInHand.Remove(playedCard);
                cardsInHand = cardsInHand.OrderCards();
                player.Hand = string.Join(",", cardsInHand);
                Game game = null;
                var roundAlert = "";
                var isLastTurn = false;
                var isGameOver = false;

                if (belaCalled) {
                    if (player.Team.Value == Team.FirstTeam) 
                    {
                        round.FirstTeamCalls += 20;
                        round.FirstTeamRoundTotal += 20;
                    }

                    if (player.Team.Value == Team.SecondTeam)
                    {
                        round.SecondTeamCalls += 20;
                        round.SecondTeamRoundTotal += 20;
                    }
                }

                if (round.CardsPlayed.Where(cp => cp.RoundPhase == round.RoundPhase).ToList().Count == 4)
                {
                    isLastTurn = round.RoundPhase == RoundPhase.EighthCard;

                    game = await _gameRepository.GetGameWithPlayersById(round.GameId);

                    ResolveTurn(round, game, isLastTurn);

                    if (isLastTurn)
                    {
                        round.CurrentPlayerToPlay = round.FirstPlayerToPlay.GetNextPosition();
                        roundAlert = await ResolveRound(round, game);
                        if (IsGameOver(game))
                        {
                            isGameOver = true;
                            await SetGameOverDataAsync(game);
                        }
                        else 
                        {
                            AddNextRound(game, round);
                            var players = game.PlayerGames.Select(pg => pg.Player).ToList();
                            FillPlayersHands(players);
                        }
                    }
                    else
                    {
                        round.RoundPhase = round.RoundPhase.GetNextPhase();
                    }
                }
                else 
                {
                    round.CurrentPlayerToPlay = round.CurrentPlayerToPlay.GetNextPosition();
                }

                if (await _gameRepository.SaveAsync())
                {
                    var currentRoundId = round.Id;
                    var currentPhase = round.RoundPhase;
                    if (isLastTurn)
                    {
                        var newRound = game.Rounds.OrderByDescending(r => r.Id).FirstOrDefault();
                        currentRoundId = newRound.Id;
                        currentPhase = newRound.RoundPhase;
                    }
                    result.Values = GetResultObjForCardPlayed(round, position, playedCard, roundAlert, currentRoundId, currentPhase, isLastTurn, isGameOver, belaCalled);
                    return result;
                }

                return Result.Fail(new string[] { "Dogodila se greška" });
            }

            return Result.Fail(new string[] { "Nevažeća karta" });
        }

        public async Task<Result> LeaveGame(int gameId, string quitUsername)
        {
            var game = await _gameRepository.GetGameWithPlayersById(gameId);
            await SetGameOverDataAsync(game, quitUsername);

            if (await _gameRepository.SaveAsync())
                return Result.Success();
            else
                return Result.Fail(new string[] { "Dogodila se greška" });
        }

        public Result LeaveGameTimerElapsed(int gameId, string connString)
        {
            string quitUsername = "", opponent1Username = "", opponent2Username = "";

            var isSaved = _gameRepository.SaveGameDataForTimerElapsed(gameId, connString, ref quitUsername, ref opponent1Username, ref opponent2Username);
            if (isSaved)
            {
                var result = Result.Success();
                result.Values = new object[] { quitUsername, opponent1Username, opponent2Username };
                return result;
            }
            else
                return Result.Fail(new string[] { "Dogodila se greška" });
        }

        private async Task SetGameOverDataAsync(Game game, string quitUsername = "")
        {
            var players = await GetPlayersWinnersFirstAsync(game, quitUsername);
            await _identityService.UpdateWinsAndLosesAsync(players, quitUsername);
            var room = await _roomRepository.GetByIdAsync(game.RoomId);
            room.InGame = false;
            game.GameStatus = GameStatus.Ended;
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
            users.ForEach(u => u.UserStatus = UserStatus.InGame);
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

        private void AddNextRound(Game game, Round previousRound)
        {
            Round round = new Round()
            {
                RoundNumber = previousRound.RoundNumber + 1,
                RoundPhase = RoundPhase.TrumpCalling,
                FirstPlayerToPlay = previousRound.FirstPlayerToPlay.GetNextPosition(),
                CurrentPlayerToPlay = previousRound.FirstPlayerToPlay.GetNextPosition()
            };
            game.Rounds.Add(round);
        }

        private void FillPlayersHands(List<Player> players)
        {
            var deck = CardsManager.GetShuffledDeck();
            players.ForEach(p => p.Hand = deck.DealCards(8));
        }

        private void AddRoundsToData(List<RoundShort> list, List<Round> rounds, Team team)
        {
            foreach (var round in rounds)
            {
                if (round != rounds.Last()) 
                {
                    list.Add(new RoundShort()
                    {
                        Number = round.RoundNumber,
                        Score1 = team == Team.FirstTeam ? round.FirstTeamRoundTotal : team == Team.SecondTeam ? round.SecondTeamRoundTotal : 0,
                        Score2 = team == Team.FirstTeam ? round.SecondTeamRoundTotal : team == Team.SecondTeam ? round.FirstTeamRoundTotal : 0
                    });
                }
            }
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

        private void FillPlayedCards(CurrentGameDataViewModel model, List<CardPlayed> cardsPlayed, PlayerPosition playerPosition)
        {
            foreach (var cardPlayed in cardsPlayed)
            {
                var onScreenPosition = playerPosition.GetOnScreenPosition(cardPlayed.PlayerPosition);
                Card card = CardsManager.GetCardFromString(cardPlayed.CardString);
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

        private void FillCalls(CurrentGameDataViewModel model, List<Call> calls, PlayerPosition playerPosition)
        {
            foreach (var pos in Enum.GetValues(typeof(PlayerPosition)))
            {
                var position = (PlayerPosition)pos;
                var callValuesForPosition = calls.Where(c => c.PlayerPosition == position).Select(c => c.Value).ToList();
                var onScreenPosition = playerPosition.GetOnScreenPosition(position);
                if (callValuesForPosition.Count > 0)
                {
                    int maxValue = callValuesForPosition.Max();
                    switch (onScreenPosition)
                    {
                        case PlayerPosition.Up:
                            model.UpCallValue = maxValue;
                            break;
                        case PlayerPosition.Left:
                            model.LeftCallValue = maxValue;
                            break;
                        case PlayerPosition.Right:
                            model.RightCallValue = maxValue;
                            break;
                        case PlayerPosition.Down:
                            model.DownCallValue = maxValue;
                            break;
                    }
                }
            }
        }

        private void FindFourOfAKindCalls(List<Card> selectedCards, PlayerPosition position, ref List<Call> foundCalls, ref List<Card> remainedCards)
        {
            List<Card> cards = new List<Card>(selectedCards);
            var countOfCardValues = new int[9];

            foreach (var card in cards) 
            {
                countOfCardValues[(int)card.Value]++;
            }

            for (int i = 1; i < 9; i++)
            {
                var cardValue = (CardValue)i;

                if (countOfCardValues[i] != 4 || cardValue == CardValue.Seven || cardValue == CardValue.Eight)
                {
                    continue;
                }

                switch (cardValue)
                {
                    case CardValue.Jack:
                        foundCalls.Add(new Call(position, CallType.FourJacks, CardsManager.GetCard(CardSuit.Spades, cardValue).ToString()));
                        break;
                    case CardValue.Nine:
                        foundCalls.Add(new Call(position, CallType.FourNines, CardsManager.GetCard(CardSuit.Spades, cardValue).ToString()));
                        break;
                    case CardValue.Ace:
                    case CardValue.King:
                    case CardValue.Queen:
                    case CardValue.Ten:
                        foundCalls.Add(new Call(position, CallType.FourOfAKind, CardsManager.GetCard(CardSuit.Spades, cardValue).ToString()));
                        break;
                }


                cards.RemoveAll(c => c.Value == cardValue);
            }

            if (cards.Count > 0)
                remainedCards.AddRange(cards);
        }

        private void FindSequenceCalls(List<Card> selectedCards, PlayerPosition position, ref List<Call> foundCalls, ref List<Card> remainedCards)
        {
            List<Card> cards = new List<Card>(selectedCards);
            var cardsBySuit = new[] { null, new List<Card>(), new List<Card>(), new List<Card>(), new List<Card>() };

            foreach (var card in cards)
            {
                cardsBySuit[(int)card.Suit].Add(card);
            }

            for (var index = 1; index < 5; index++)
            {
                var currentSuitCards = cardsBySuit[index];

                if (currentSuitCards.Count < 3)
                {
                    continue;
                }

                currentSuitCards.Sort((c1, c2) => c1.Value.CompareTo(c2.Value));
                var previousValue = (int)currentSuitCards[0].Value;
                var counter = 1;
                for (var i = 1; i < currentSuitCards.Count; i++)
                {
                    if ((int)currentSuitCards[i].Value == previousValue + 1)
                    {
                        counter++;
                    }
                    else 
                    {
                        switch (counter)
                        {
                            case 3:
                                foundCalls.Add(new Call(position, CallType.ThreeInARow, currentSuitCards[i - 1].ToString()));
                                break;
                            case 4:
                                foundCalls.Add(new Call(position, CallType.FourInARow, currentSuitCards[i - 1].ToString()));
                                break;
                            case 5:
                                foundCalls.Add(new Call(position, CallType.FiveInARow, currentSuitCards[i - 1].ToString()));
                                break;
                            case 6:
                                foundCalls.Add(new Call(position, CallType.SixInARow, currentSuitCards[i - 1].ToString()));
                                break;
                        }

                        if(counter > 2)
                            cards.RemoveAll(c => c.Suit == currentSuitCards[i - 1].Suit && c.Value <= currentSuitCards[i - 1].Value);

                        counter = 1;
                    }

                    previousValue = (int)currentSuitCards[i].Value;
                }

                switch (counter)
                {
                    case 3:
                        foundCalls.Add(new Call(position, CallType.ThreeInARow, currentSuitCards[currentSuitCards.Count - 1].ToString()));
                        break;
                    case 4:
                        foundCalls.Add(new Call(position, CallType.FourInARow, currentSuitCards[currentSuitCards.Count - 1].ToString()));
                        break;
                    case 5:
                        foundCalls.Add(new Call(position, CallType.FiveInARow, currentSuitCards[currentSuitCards.Count - 1].ToString()));
                        break;
                    case 6:
                        foundCalls.Add(new Call(position, CallType.SixInARow, currentSuitCards[currentSuitCards.Count - 1].ToString()));
                        break;
                    case 7:
                        foundCalls.Add(new Call(position, CallType.SevenInARow, currentSuitCards[currentSuitCards.Count - 1].ToString()));
                        break;
                    case 8:
                        foundCalls.Add(new Call(position, CallType.EightInARow, currentSuitCards[currentSuitCards.Count - 1].ToString()));
                        break;
                }

                if (counter > 2)
                    cards.RemoveAll(c => c.Suit == currentSuitCards[currentSuitCards.Count - 1].Suit && c.Value <= currentSuitCards[currentSuitCards.Count - 1].Value);
            }

            if (cards.Count > 0)
                remainedCards.AddRange(cards);
        }

        private bool DoListsHaveMatchingCard(List<Card> firstList, List<Card> secondList)
        {
            return firstList.Any(c => secondList.Contains(c));
        }

        private object[] GetResultObjForTrumpSelection(Round round)
        {
            return new object[]
            {
                round.GameId.ToString(),
                new {
                    roundPhase = (int)round.RoundPhase,
                    positionToPlayUp = (int)PlayerPosition.Up.GetOnScreenPosition(round.CurrentPlayerToPlay),
                    positionToPlayDown = (int)PlayerPosition.Down.GetOnScreenPosition(round.CurrentPlayerToPlay),
                    positionToPlayLeft = (int)PlayerPosition.Left.GetOnScreenPosition(round.CurrentPlayerToPlay),
                    positionToPlayRight = (int)PlayerPosition.Right.GetOnScreenPosition(round.CurrentPlayerToPlay),
                    selectedTrump = round.CurrentTrump.HasValue ? (int)round.CurrentTrump : 0,
                    trumpSelectedBy = round.TrumpSelectedBy,
                    isLast = round.CurrentPlayerToPlay == round.FirstPlayerToPlay.GetPreviousPosition()
                }
            };
        }

        private object[] GetResultObjForCall(Round round, int highestCallValue = 0, PlayerPosition? winningCallPosition = null)
        {
            return new object[]
            {
                round.GameId.ToString(),
                new {
                    roundPhase = (int)round.RoundPhase,
                    positionToPlayUp = (int)PlayerPosition.Up.GetOnScreenPosition(round.CurrentPlayerToPlay),
                    positionToPlayDown = (int)PlayerPosition.Down.GetOnScreenPosition(round.CurrentPlayerToPlay),
                    positionToPlayLeft = (int)PlayerPosition.Left.GetOnScreenPosition(round.CurrentPlayerToPlay),
                    positionToPlayRight = (int)PlayerPosition.Right.GetOnScreenPosition(round.CurrentPlayerToPlay),
                    isLast = round.CurrentPlayerToPlay == round.FirstPlayerToPlay.GetPreviousPosition(),
                    callValue = highestCallValue,
                    positionThatCalledUp = (int)PlayerPosition.Up.GetOnScreenPosition(round.CurrentPlayerToPlay.GetPreviousPosition()),
                    positionThatCalledDown = (int)PlayerPosition.Down.GetOnScreenPosition(round.CurrentPlayerToPlay.GetPreviousPosition()),
                    positionThatCalledLeft = (int)PlayerPosition.Left.GetOnScreenPosition(round.CurrentPlayerToPlay.GetPreviousPosition()),
                    positionThatCalledRight = (int)PlayerPosition.Right.GetOnScreenPosition(round.CurrentPlayerToPlay.GetPreviousPosition()),
                    winningCallPositionUp = winningCallPosition.HasValue ? (int)PlayerPosition.Up.GetOnScreenPosition(winningCallPosition.Value) : 0,
                    winningCallPositionDown = winningCallPosition.HasValue ? (int)PlayerPosition.Down.GetOnScreenPosition(winningCallPosition.Value) : 0,
                    winningCallPositionLeft = winningCallPosition.HasValue ? (int)PlayerPosition.Left.GetOnScreenPosition(winningCallPosition.Value) : 0,
                    winningCallPositionRight = winningCallPosition.HasValue ? (int)PlayerPosition.Right.GetOnScreenPosition(winningCallPosition.Value) : 0,
                    winningCallPositionReal = winningCallPosition.HasValue ? winningCallPosition.Value : 0
                }
            };
        }

        private object[] GetResultObjForCardPlayed(Round round, PlayerPosition positionThatPlayedCard, Card playedCard, string roundAlert, int currentRoundId, RoundPhase currentPhase, bool isLastTurn, bool isGameOver, bool belaCalled)
        {
            return new object[]
            {
                round.GameId.ToString(),
                new {
                    roundPhase = (int)currentPhase,
                    currentRoundId = currentRoundId,
                    isNewRound = isLastTurn,
                    positionToPlayUp = (int)PlayerPosition.Up.GetOnScreenPosition(round.CurrentPlayerToPlay),
                    positionToPlayDown = (int)PlayerPosition.Down.GetOnScreenPosition(round.CurrentPlayerToPlay),
                    positionToPlayLeft = (int)PlayerPosition.Left.GetOnScreenPosition(round.CurrentPlayerToPlay),
                    positionToPlayRight = (int)PlayerPosition.Right.GetOnScreenPosition(round.CurrentPlayerToPlay),
                    positionThatPlayedCardUp = (int)PlayerPosition.Up.GetOnScreenPosition(positionThatPlayedCard),
                    positionThatPlayedCardDown = (int)PlayerPosition.Down.GetOnScreenPosition(positionThatPlayedCard),
                    positionThatPlayedCardLeft = (int)PlayerPosition.Left.GetOnScreenPosition(positionThatPlayedCard),
                    positionThatPlayedCardRight = (int)PlayerPosition.Right.GetOnScreenPosition(positionThatPlayedCard),
                    dealerPositionUp = isLastTurn ? (int)PlayerPosition.Up.GetOnScreenPosition(round.CurrentPlayerToPlay.GetPreviousPosition()) : 0,
                    dealerPositionDown = isLastTurn ? (int)PlayerPosition.Down.GetOnScreenPosition(round.CurrentPlayerToPlay.GetPreviousPosition()) : 0,
                    dealerPositionLeft = isLastTurn ? (int)PlayerPosition.Left.GetOnScreenPosition(round.CurrentPlayerToPlay.GetPreviousPosition()) : 0,
                    dealerPositionRight = isLastTurn ? (int)PlayerPosition.Right.GetOnScreenPosition(round.CurrentPlayerToPlay.GetPreviousPosition()) : 0,
                    playedCardUrl = playedCard.ImgPath,
                    roundAlert = roundAlert,
                    belaCalled = belaCalled,
                    isGameOver = isGameOver
                },
                isGameOver
            };
        }

        private async Task ResolveCalls(Round round, PlayerPosition position)
        {
            var allCalls = round.Calls;
            var game = await _gameRepository.GetGameWithPlayersById(round.GameId);
            var partnerPosition = position.GetTeammatePosition();

            var sumOfCalls = allCalls.Where(c => c.PlayerPosition == position || c.PlayerPosition == partnerPosition).Select(c => c.Value).Sum();
            var playerThatWonCalls = game.PlayerGames.Where(pg => pg.Player.PlayerPosition == position).Select(pg => pg.Player).FirstOrDefault();
            SetCallValuesInRound(round, playerThatWonCalls.Team.Value, sumOfCalls);
        }

        private void ResolveTurn(Round round, Game game, bool isLastTurn)
        {
            var cardsPlayed = round.CardsPlayed.Where(cp => cp.RoundPhase == round.RoundPhase).ToList();
            var sum = cardsPlayed.Sum(cp => cp.Value);
            var cards = cardsPlayed.Select(cp => CardsManager.GetCardFromString(cp.CardString)).ToList();
            var strongestCard = GetStrongestCardInTurn(cards, round.CurrentTrump.Value);
            var winningPosition = round.CardsPlayed.Where(cp => strongestCard.ToString() == cp.CardString).FirstOrDefault().PlayerPosition;
            var playerThatWonTurn = game.PlayerGames.Where(pg => pg.Player.PlayerPosition == winningPosition).Select(pg => pg.Player).FirstOrDefault();
            SetPointValuesInTurn(round, playerThatWonTurn.Team.Value, sum, isLastTurn);
            round.CurrentPlayerToPlay = winningPosition;
        }

        private async Task<string> ResolveRound(Round round, Game game)
        {
            var playerThatCalledTrump = await _playerRepository.GetPlayerByUsernameAsync(round.TrumpSelectedBy);
            var teamThatCalledTrump = playerThatCalledTrump.Team.Value;
            var totalRoundPoints = round.FirstTeamRoundTotal + round.SecondTeamRoundTotal;
            if ((teamThatCalledTrump == Team.FirstTeam && round.SecondTeamRoundTotal >= totalRoundPoints / 2) || round.FirstTeamScore == 0)
            {
                game.SecondTeamTotalScore += round.FirstTeamScore == 0 ? totalRoundPoints + 90 : totalRoundPoints;
                round.SecondTeamRoundTotal = round.FirstTeamScore == 0 ? totalRoundPoints + 90 : totalRoundPoints;
                round.FirstTeamRoundTotal = 0;
                var username = playerThatCalledTrump.UserName;
                var teammateUsername = game.PlayerGames.Where(pg => pg.Player.Team == Team.FirstTeam).Select(pg => pg.Player.UserName).Where(u => u != username).FirstOrDefault();
                return $"{username} i {teammateUsername} su pali";
            }
            else if ((teamThatCalledTrump == Team.SecondTeam && round.FirstTeamRoundTotal >= totalRoundPoints / 2) || round.SecondTeamScore == 0)
            {
                game.FirstTeamTotalScore += round.SecondTeamScore == 0 ? totalRoundPoints + 90 : totalRoundPoints;
                round.FirstTeamRoundTotal = round.SecondTeamScore == 0 ? totalRoundPoints + 90 : totalRoundPoints;
                round.SecondTeamRoundTotal = 0;
                var username = playerThatCalledTrump.UserName;
                var teammateUsername = game.PlayerGames.Where(pg => pg.Player.Team == Team.SecondTeam).Select(pg => pg.Player.UserName).Where(u => u != username).FirstOrDefault();
                return $"{username} i {teammateUsername} su pali";
            }
            else
            {
                game.FirstTeamTotalScore += round.FirstTeamRoundTotal;
                game.SecondTeamTotalScore += round.SecondTeamRoundTotal;
                return "";
            }
        }

        private Card GetStrongestCardInTurn(List<Card> cards, CardSuit trump)
        {
            if (cards.Any(c => c.Suit == trump))
            {
                return cards.Where(c => c.Suit == trump).OrderByDescending(c => c.GetValueOrder(true)).FirstOrDefault();
            }

            var firstCardSuit = cards[0].Suit;
            return cards.Where(c => c.Suit == firstCardSuit).OrderByDescending(c => c.GetValueOrder(false)).FirstOrDefault();
        }

        private PlayerPosition GetWiningCallPosition(Round round)
        {
            Call call = null;

            var orderedCalls = round.Calls.OrderByDescending(c => c.Type);
            var firstCall = orderedCalls.FirstOrDefault();

            if (firstCall.Type == CallType.EightInARow || firstCall.Type == CallType.FourJacks || firstCall.Type == CallType.FourNines)
                call = firstCall;

            if (firstCall.Type == CallType.FourOfAKind)
            {
                call = orderedCalls
                        .ThenBy(c => Array.IndexOf(CustomSorting.FourOfAKindSortOrder, CardsManager.GetCardFromString(c.HighestCard).Value))
                        .FirstOrDefault();
            }
                
            if (firstCall.Type == CallType.SevenInARow || firstCall.Type == CallType.SixInARow || firstCall.Type == CallType.FiveInARow || firstCall.Type == CallType.FourInARow || firstCall.Type == CallType.ThreeInARow)
            {
                var maxValue = round.Calls.Select(c => c.Value).Max();
                call = orderedCalls
                        .Where(c => c.Value == maxValue)
                        .OrderByDescending(c => CardsManager.GetCardFromString(c.HighestCard).Value)
                        .ThenBy(c => Array.IndexOf(CustomSorting.GetPositionSortingOrderForCalls(round.FirstPlayerToPlay), c.PlayerPosition))
                        .FirstOrDefault();
            }

            return call.PlayerPosition;
        }

        private void SetCallValuesInRound(Round round, Team team, int value)
        {
            switch (team)
            {
                case Team.FirstTeam:
                    round.FirstTeamCalls = value;
                    round.FirstTeamRoundTotal = value;
                    break;
                case Team.SecondTeam:
                    round.SecondTeamCalls = value;
                    round.SecondTeamRoundTotal = value;
                    break;
            }
        }

        private void SetPointValuesInTurn(Round round, Team team, int value, bool isLastTurn)
        {
            switch (team)
            {
                case Team.FirstTeam:
                    round.FirstTeamScore += isLastTurn ? value + 10 : value;
                    round.FirstTeamRoundTotal += isLastTurn ? value + 10 : value;
                    break;
                case Team.SecondTeam:
                    round.SecondTeamScore += isLastTurn ? value + 10 : value;
                    round.SecondTeamRoundTotal += isLastTurn ? value + 10 : value;
                    break;
            }
        }

        private List<string> FindCallCardUrlsFromHighestCard(Card card, CallType type)
        {
            List<string> urls = new List<string>();

            switch (type)
            {
                case CallType.FourJacks:
                case CallType.FourNines:
                case CallType.FourOfAKind:
                    urls.AddRange(CardsManager.GetFourOfAKind(card.Value).Select(c => c.ImgPath).ToList());
                    break;
                case CallType.EightInARow:
                case CallType.SevenInARow:
                case CallType.SixInARow:
                case CallType.FiveInARow:
                case CallType.FourInARow:
                case CallType.ThreeInARow:
                    urls.AddRange(CardsManager.GetSequence(card.Suit, card.Value, type).Select(c => c.ImgPath).ToList());
                    break;
            }

            return urls;
        }

        private bool IsGameOver(Game game)
        {
            return game.FirstTeamTotalScore > 1001 || game.SecondTeamTotalScore > 1001;
        }

        private async Task<List<Player>> GetPlayersWinnersFirstAsync(Game game, string quitUsername) 
        {
            List<Player> result = null;
            if (quitUsername != "")
            {
                var quitPlayer = await _playerRepository.GetPlayerByUsernameAsync(quitUsername);
                var query = game.PlayerGames.Select(pg => pg.Player);
                result = quitPlayer.Team.Value == Team.FirstTeam ? query.OrderByDescending(p => p.Team).ToList() :
                         quitPlayer.Team.Value == Team.SecondTeam ? query.OrderBy(p => p.Team).ToList() : query.ToList();
                return result;

            }

            if (game.FirstTeamTotalScore > game.SecondTeamTotalScore)
            {
                result = game.PlayerGames
                    .Select(pg => pg.Player)
                    .OrderBy(p => p.Team)
                    .ToList();
            }

            if (game.SecondTeamTotalScore > game.FirstTeamTotalScore)
            {
                result = game.PlayerGames
                    .Select(pg => pg.Player)
                    .OrderByDescending(p => p.Team)
                    .ToList();
            }

            return result;
        }
    }
}
