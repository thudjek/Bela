using Bela.Application.Interfaces;
using Bela.WebMVC.ComponentModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bela.WebMVC.Extensions;
using System.Security.Claims;

namespace Bela.WebMVC.Views.Shared.Components.RoomList
{
    public class RoomListViewComponent : ViewComponent
    {
        private readonly IRoomService _roomService;
        public RoomListViewComponent(IRoomService roomService)
        {
            _roomService = roomService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string filterRoomName)
        {
            RoomVCModel model = new RoomVCModel();
            var user = (ClaimsPrincipal)User;
            var userId = user.GetUserId();
            model.Rooms = await _roomService.GetRoomListViewModelsAsync(userId, filterRoomName);

            return View(model);
        }
    }
}
