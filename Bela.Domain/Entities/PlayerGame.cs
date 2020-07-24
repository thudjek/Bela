using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Domain.Entities
{
    public class PlayerGame
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }

    }
}
