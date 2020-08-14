using Bela.Application.Utility;
using Bela.Application.ViewModels.Room;
using Bela.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bela.Application.Interfaces
{
    public interface IRoomService
    {
        Task<int> CreateRoomAsync(CreateRoomModel model, int userId);
        Task<RoomViewModel> GetRoomViewModelAsync(int roomId, int userId, string username);
        Task<bool> LeaveRoom(int roomId, int userId);
        Task<Result> DropRoom(int roomId, int userId);
        Task<Result> KickUserFromRoom(int userId, int roomId);
        Task<List<RoomListViewModel>> GetRoomListViewModelsAsync(int userId, string filterRoomName);
        Task<Result> TryJoinRoomAsync(int roomId, int userId, bool isPrivate, string roomPassword, bool isInvite);
        Task<List<UserInRoomViewModel>> GetUserInRoomViewModelsAsync(int roomId);
        Task<bool> ToggleReady(int roomId, int userId);
        Task<bool> SwapPlayers(int firstUserId, int secondUserId, int roomId);
        Task<Result> CanGameBeStarted(int roomId);
    }
}
