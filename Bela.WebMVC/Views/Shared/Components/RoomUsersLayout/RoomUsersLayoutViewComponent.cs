using Bela.Application.Interfaces;
using Bela.WebMVC.ComponentModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bela.Application.ViewModels.User;

namespace Bela.WebMVC.Views.Shared.Components.RoomUsersLayout
{
    public class RoomUsersLayoutViewComponent : ViewComponent
    {
        private readonly IRoomService _roomService;
        public RoomUsersLayoutViewComponent(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int roomId, bool isOwner)
        {
            RoomUsersLayoutVCModel model = new RoomUsersLayoutVCModel();
            model.Users = await _roomService.GetUserInRoomViewModelsAsync(roomId);
            model.IsOwner = isOwner;
            return View(model);
        }
    }
}
