using Bela.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Application.ViewModels.Game
{
    public class GameViewModel
    {
        public int Id { get; set; }
        public string UserNameUp { get; set; }
        public string UserNameDown { get; set; }
        public string UserNameLeft { get; set; }
        public string UserNameRight { get; set; }
        public PlayerPosition Position { get; set; }
    }
}
