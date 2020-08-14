using System;
using System.Collections.Generic;
using System.Text;
using Bela.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Bela.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public Gender Gender { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Dropouts { get; set; }
        public UserStatus UserStatus { get; set; }
        public Player Player { get; set; }
        public DateTime? CameOnline { get; set; }
        public DateTime? LastSeenOnline { get; set; }
        public int? RoomId { get; set; }
        public Room Room { get; set; }
        public bool IsReady { get; set; }
        public int? RoomOrderNumber { get; set; }
        public string MainHubConnectionId { get; set; }
    }
}
