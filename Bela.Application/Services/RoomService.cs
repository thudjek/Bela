using AutoMapper;
using Bela.Application.Interfaces;
using Bela.Application.Utility;
using Bela.Application.ViewModels.Room;
using Bela.Application.ViewModels.User;
using Bela.Domain.Entities;
using Bela.Domain.Enums;
using Bela.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bela.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public RoomService(
            IRoomRepository roomRepository,
            UserManager<User> userManager,
            IMapper mapper)
        {
            _roomRepository = roomRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<int> CreateRoomAsync(CreateRoomModel model, int userId)
        {
            Room room = _mapper.Map<CreateRoomModel, Room>(model);
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if(user != null)
            {
                room.OwnerId = userId;
                room.Users.Add(user);
                user.UserStatus = UserStatus.InRoom;
                user.RoomOrderNumber = 1;
                _roomRepository.CreateRoom(room);
                await _roomRepository.SaveAsync();
            }

            return room.Id;
        }

        public async Task<RoomViewModel> GetRoomViewModelAsync(int roomId, int userId, string username)
        {
            var room = await _roomRepository.GetByIdAsync(roomId);
            var model = _mapper.Map<Room, RoomViewModel>(room);
            if (room.OwnerId == userId)
            {
                model.IsOwner = true;
                model.OwnerUsername = username;
            }
            return model;
        }

        public async Task<bool> LeaveRoom(int roomId, int userId)
        {
            var room = await _roomRepository.GetByIdWithUsersAsync(roomId);
            var user = room.Users.FirstOrDefault(u => u.Id == userId);
            room.Users.Remove(user);
            user.UserStatus = UserStatus.Online;
            user.RoomOrderNumber = null;
            user.IsReady = false;
            return await _roomRepository.SaveAsync();
        }

        public async Task<Result> DropRoom(int roomId, int userId)
        {
            var room = await _roomRepository.GetByIdWithUsersAsync(roomId);
            if (room.OwnerId == userId)
            {
                _roomRepository.DeleteRoom(room);
                room.Users.ForEach(u => 
                {
                    if(u.UserStatus == UserStatus.InRoom)
                        u.UserStatus = UserStatus.Online;

                    u.RoomOrderNumber = null;
                    u.IsReady = false;
                });
            }

            if (await _roomRepository.SaveAsync())
            {
                var result = Result.Success();
                List<string> connIds = room.Users.Where(u => u.Id != userId).Select(u => u.MainHubConnectionId).Where(conn => !string.IsNullOrEmpty(conn)).ToList();
                result.Values = new object[] { string.Join(",", connIds) };
                return result;
            }
            else
                return Result.Fail(new string[] { });
        }

        public async Task<Result> KickUserFromRoom(int userId, int roomId)
        {
            var room = _roomRepository.GetByIdWithUsers(roomId);
            var user = room.Users.FirstOrDefault(u => u.Id == userId);
            room.Users.Remove(user);

            if(user.UserStatus == UserStatus.InRoom)
                user.UserStatus = UserStatus.Online;

            user.RoomOrderNumber = null;
            user.IsReady = false;
            if (await _roomRepository.SaveAsync())
            {
                var result = Result.Success();
                result.Values = new object[] { user.MainHubConnectionId };
                return result;
            }
            else
                return Result.Fail(new string[] { });
        }

        public async Task<List<RoomListViewModel>> GetRoomListViewModelsAsync(int userId, string filterRoomName)
        {
            var roomList = await _roomRepository.GetRoomListAsync(userId, filterRoomName);
            var viewModelList = _mapper.Map<List<Room>, List<RoomListViewModel>>(roomList);
            return viewModelList;
        }

        public async Task<Result> TryJoinRoomAsync(int roomId, int userId, bool isPrivate, string roomPassword, bool isInvite)
        {
            var room = await _roomRepository.GetByIdWithUsersAsync(roomId);
            if (room != null)
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (IsUserInAnotherRoom(user, roomId))
                    return Result.Fail(new string[] { "Već se nalazite u drugoj sobi" });

                if (IsUserInThisRoom(room, userId))
                    return Result.Success();

                if (!isInvite && isPrivate && !IsPasswordCorrect(room, roomPassword))
                    return Result.Fail(new string[] { "Pogrešna lozinka sobe" });

                if (IsRoomFull(room))
                    return Result.Fail(new string[] { "Soba je popunjena" });

                user.UserStatus = UserStatus.InRoom;
                user.RoomOrderNumber = GetAvailableRoomOrderNumber(room.Users.Select(r => r.RoomOrderNumber).ToList());
                room.Users.Add(user);

                if (await _roomRepository.SaveAsync())
                    return Result.Success();
                else
                    return Result.Fail(new string[] { "Dogodila se greška" });
            }
            return Result.Fail(new string[] { "Soba je raspuštena" });
        }

        public async Task<List<UserInRoomViewModel>> GetUserInRoomViewModelsAsync(int roomId)
        {
            List<UserInRoomViewModel> list = new List<UserInRoomViewModel>();
            var room = await _roomRepository.GetByIdWithUsersAsync(roomId);
            if (room != null)
            {
                list = _mapper.Map<List<User>, List<UserInRoomViewModel>>(room.Users);
            }
            return list;
        }

        public async Task<bool> ToggleReady(int roomId, int userId)
        {
            var room = await _roomRepository.GetByIdWithUsersAsync(roomId);
            var user = room.Users.Where(u => u.Id == userId).FirstOrDefault();
            user.IsReady = !user.IsReady;
            return await _roomRepository.SaveAsync();
        }

        public async Task<bool> SwapPlayers(int firstUserId, int secondUserId, int roomId)
        {
            var room = await _roomRepository.GetByIdWithUsersAsync(roomId);
            var firstUser = room.Users.Where(u => u.Id == firstUserId).FirstOrDefault();
            var secondUser = room.Users.Where(u => u.Id == secondUserId).FirstOrDefault();

            int temp = secondUser.RoomOrderNumber.Value;
            secondUser.RoomOrderNumber = firstUser.RoomOrderNumber;
            firstUser.RoomOrderNumber = temp;

            return await _roomRepository.SaveAsync();
        }

        public async Task<Result> CanGameBeStarted(int roomId)
        {
            var room = await _roomRepository.GetByIdWithUsersAsync(roomId);
            if(room.Users.Count < 4)
                return Result.Fail(new string[] { "Potrebna su 4 igrača za igru" });

            if(!AreAllPlayersReady(room.Users))
                return Result.Fail(new string[] { "Nisu svi igrači spremni" });

            return Result.Success();
        }

        private bool IsUserInAnotherRoom(User user, int roomId)
        {
            if (user.RoomId.HasValue)
                return user.RoomId.Value != roomId;

            return false;
        }

        private bool IsUserInThisRoom(Room room, int userId)
        {
            return room.Users.Any(u => u.Id == userId);
        }

        private bool IsPasswordCorrect(Room room, string roomPassword)
        {
            return room.RoomPassword == roomPassword;
        }

        private bool IsRoomFull(Room room)
        {
            return room.Users.Count >= 4;
        }

        private int GetAvailableRoomOrderNumber(List<int?> nums)
        {
            if (!nums.Contains(2))
                return 2;

            if (!nums.Contains(3))
                return 3;

            return 4;
        }

        private bool AreAllPlayersReady(List<User> users)
        {
            return users.Where(u => u.RoomOrderNumber != 1).All(u => u.IsReady);
        }
    }
}
