using Bela.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bela.Domain.Interfaces
{
    public interface IRoomRepository : IBaseRepository
    {
        void CreateRoom(Room room);
        Task<Room> GetByIdAsync(int id);
        Task<Room> GetByIdWithUsersAsync(int id);
        Task<List<Room>> GetRoomListAsync(int userId, string filterRoomName);
        void DeleteRoom(Room room);
    }
}
