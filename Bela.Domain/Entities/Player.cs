using Bela.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Domain.Entities
{
    public class Player : BaseEntity
    {
        public Player()
        {
            PlayerGames = new List<PlayerGame>();
        }

        public int UserId { get; set; }
        public User User { get; set; }
        public string UserName { get; set; }
        public Team? Team { get; set; }
        public PlayerPosition? PlayerPosition { get; set; }
        public string Hand { get; set; }
        public List<PlayerGame> PlayerGames { get; set; }
    }
}
