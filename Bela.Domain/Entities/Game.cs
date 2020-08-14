using Bela.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Domain.Entities
{
    public class Game : BaseEntity
    {
        public Game()
        {
            Rounds = new List<Round>();
            PlayerGames = new List<PlayerGame>();
        }

        public int FirstTeamTotalScore { get; set; }
        public int SecondTeamTotalScore { get; set; }
        public GameStatus GameStatus { get; set; }
        public List<PlayerGame> PlayerGames { get; set; }
        public List<Round> Rounds { get; set; }
    }
}
