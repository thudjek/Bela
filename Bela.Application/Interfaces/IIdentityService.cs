using Bela.Application.Utility;
using Bela.Application.ViewModels.User;
using Bela.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bela.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<Result> RegisterUser(UserRegisterModel model);
        Task<Result> LogInUser(UserLoginModel model);
        Task LogOutUser(int userId);
        Task<Result> ConfirmEmailActivation(string userId, string token);
        Task<Result> GetPasswordResetResult(string email);
        Task<Result> ResetPassword(ResetPasswordModel model);
        Task<int> GetUsersRoomId(int userId);
        List<UserListViewModel> GetUserListViewModels(int userId, string filterUsername);
        Task<UserDetailsModel> GetUserDetailsModelAsync(int userId);
        Task<string> GetUsersMainHubConnectionId(int userId);
        Task SetUsersMainHubConnectionId(int userId, string connectionId);
        Task DeleteUsersMainHubConnectionId(int userId);
    }
}
