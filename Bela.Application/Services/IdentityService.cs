using Bela.Application.Interfaces;
using Bela.Application.Utility;
using Bela.Application.ViewModels.User;
using Bela.Application.Extensions;
using Bela.Domain.Entities;
using Bela.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bela.Domain.Enums;
using System.Security.Policy;
using System.Linq;

namespace Bela.Application.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;

        public IdentityService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IPlayerRepository playerRepository,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _playerRepository = playerRepository;
            _mapper = mapper;
        }

        public async Task<Result> RegisterUser(UserRegisterModel model)
        {
            var user = _mapper.Map<UserRegisterModel, User>(model);
            user.RegistrationDate = DateTime.Now;
            var identityResult = await _userManager.CreateAsync(user, model.Password);
            var result = identityResult.ToResult();
            if (result.IsSucessfull)
            {
                Player player = new Player() { UserId = user.Id, UserName = user.UserName };
                _playerRepository.CreatePlayer(player);
                await _playerRepository.SaveAsync();
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                result.Values = new object[] { new { userId = user.Id, token } };
                
            }
            return result;
        }

        public async Task<Result> LogInUser(UserLoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
                return Result.Fail(new string[] { "Pogrešno korisničko ime ili lozinka" });

            var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            return await CheckSignInResult(user, signInResult);
        }

        public async Task LogOutUser(int userId, bool isManualLogOut = true)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                await UpdateOfflineUser(user);
            }

            if (isManualLogOut)
                await _signInManager.SignOutAsync();
            else
                await Task.FromResult(0);
        }

        public async Task<Result> ConfirmEmailActivation(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var identityResult = await _userManager.ConfirmEmailAsync(user, token);
                return identityResult.ToResult();
            }
            return Result.Fail(new string[] { });
        }

        public async Task<Result> GetPasswordResetResult(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = Result.Success();
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                result.Values = new object[] { new { token, email } };
                return result;
            }
            return Result.Fail(new string[] { });
        }

        public async Task<Result> ResetPassword(ResetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var identityResult = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                return identityResult.ToResult();
            }
            return Result.Fail(new string[] { });
        }

        public async Task<int> GetUsersRoomId(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null && user.RoomId.HasValue)
                return user.RoomId.Value;

            return 0;
        }

        private async Task<Result> CheckSignInResult(User user, SignInResult signInResult)
        {
            if (signInResult.Succeeded)
            {
                await UpdateOnlineUser(user);
                return Result.Success();
            }

            if (signInResult.IsNotAllowed)
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    return Result.Fail(new string[] { "Email nije potvrđen" });
                }
                else if (!await _userManager.IsPhoneNumberConfirmedAsync(user))
                {
                    return Result.Fail(new string[] { "Broj telefona nije potvrđen" });
                }
            }

            if (signInResult.IsLockedOut)
                return Result.Fail(new string[] { "Račun je blokiran" });

            if (signInResult.RequiresTwoFactor)
                return Result.Fail(new string[] { "Potrebna \"2 Factor\" autentikacija" });

            return Result.Fail(new string[] { "Pogrešno korisničko ime ili lozinka" });
        }

        public List<UserListViewModel> GetUserListViewModels(int userId, string filterUsername)
        {
            var query = _userManager.Users
                        .Where(u => u.UserStatus != UserStatus.Offline && u.Id != userId);

            if (!string.IsNullOrEmpty(filterUsername))
                query = query.Where(u => u.UserName.StartsWith(filterUsername));

            var userList = query
                    .OrderByDescending(u => u.CameOnline.Value)
                    .ToList();

            var viewModelList = _mapper.Map<List<User>, List<UserListViewModel>>(userList);
            return viewModelList;
        }

        public async Task<UserDetailsModel> GetUserDetailsModelAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var userDetailsModel = _mapper.Map<User, UserDetailsModel>(user);
            return userDetailsModel;
        }

        public async Task<string> GetUsersMainHubConnectionId(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return user.MainHubConnectionId;
        }

        public async Task SetUsersMainHubConnectionId(int userId, string connectionId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            user.MainHubConnectionId = connectionId;
            if(user.UserStatus == UserStatus.Offline)
            {
                if (user.RoomId.HasValue)
                    user.UserStatus = UserStatus.InRoom;
                else
                    user.UserStatus = UserStatus.Online;
            }
            await _userManager.UpdateAsync(user);
        }

        public async Task<Result> DeleteUsersMainHubConnectionId(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            user.MainHubConnectionId = null;
            if (user.UserStatus == UserStatus.InRoom && user.IsReady == true)
            {
                user.IsReady = false;
            }
                
            var identityResult = await _userManager.UpdateAsync(user);
            var result = identityResult.ToResult();

            if (result.IsSucessfull && user.RoomId.HasValue)
                result.Values = new object[] { user.RoomId };

            return result;
        }

        public async Task<Result> SetUserIsReady(int userId, bool isReady)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if(user.IsReady != isReady)
                user.IsReady = isReady;

            var identityResult = await _userManager.UpdateAsync(user);

            var result = identityResult.ToResult();
            if (result.IsSucessfull && user.RoomId.HasValue)
                result.Values = new object[] { user.RoomId };

            return result;
        }

        private async Task UpdateOnlineUser(User user)
        {
            user.UserStatus = UserStatus.Online;
            user.CameOnline = DateTime.Now;
            await _userManager.UpdateAsync(user);
        }

        private async Task UpdateOfflineUser(User user)
        {
            user.UserStatus = UserStatus.Offline;
            user.LastSeenOnline = DateTime.Now;
            await _userManager.UpdateAsync(user);
        }
    }
}
