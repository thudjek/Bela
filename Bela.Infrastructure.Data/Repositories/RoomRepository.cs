using Bela.Domain.Entities;
using Bela.Domain.Interfaces;
using Bela.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bela.Infrastructure.Data.Repositories
{
    public class RoomRepository : BaseRepository, IRoomRepository
    {
        public RoomRepository(BelaDbContext dbContext) : base(dbContext)
        {

        }

        public void CreateRoom(Room room)
        {
            if (room == null)
                throw new ArgumentNullException(nameof(room));

            _dbContext.Rooms.Add(room);
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            return await _dbContext.Rooms.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Room> GetByIdWithUsersAsync(int id)
        {
            return await _dbContext.Rooms
                        .Include(r => r.Users)
                        .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Room>> GetRoomListAsync(int userId, string filterRoomName)
        {
            var query = _dbContext.Rooms
                        .Include(r => r.Users)
                        .Where(r => r.InGame == false && !r.Users.Any(u => u.Id == userId));

            if (!string.IsNullOrEmpty(filterRoomName))
                query = query.Where(r => r.RoomName.StartsWith(filterRoomName));

            return await query.ToListAsync();
        }

        public void DeleteRoom(Room room)
        {
            _dbContext.Remove(room);
        }
    }
}
