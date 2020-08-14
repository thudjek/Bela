using Bela.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Domain.Entities
{
    public class Room : BaseEntity
    {
        public Room()
        {
            Users = new List<User>();
        }
        public string RoomName { get; set; }
        public bool IsPrivate { get; set; }
        public string RoomPassword { get; set; }
        public bool InGame { get; set; }
        public int OwnerId { get; set; }
        public List<User> Users { get; set; }
    }
}
