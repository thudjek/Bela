using Bela.Application.Interfaces;
using Bela.WebMVC.ComponentModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bela.WebMVC.Extensions;

namespace Bela.WebMVC.Views.Shared.Components.UserList
{
    public class UserListViewComponent : ViewComponent
    {
        public readonly IIdentityService _identityService;
        public UserListViewComponent(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public Task<IViewComponentResult> InvokeAsync(string filterUsername, bool isRoomAndOwner)
        {
            UserVCModel model = new UserVCModel();
            var user = (ClaimsPrincipal)User;
            var userId = user.GetUserId();
            model.Users = _identityService.GetUserListViewModels(userId, filterUsername);
            model.isRoomAndOwner = isRoomAndOwner;

            return Task.FromResult<IViewComponentResult>(View(model));
        }
    }
}
