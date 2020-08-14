using Bela.Domain.Entities;
using Bela.Domain.Enums;
using Bela.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bela.Infrastructure.Data.Utility
{
    public static class DataSeeder
    {
        public async static Task SeedUsers(UserManager<User> userManager, IPlayerRepository playerRepository) 
        {
            var username = await userManager.FindByNameAsync("popay");
            if (username == null)
            {
                User user = new User
                {
                    UserName = "popay",
                    Email = "popay@popay.com",
                    Gender = Gender.Male,
                    RegistrationDate = DateTime.Now,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "asdasd");
                if (result.Succeeded)
                {
                    Player player = new Player
                    {
                        UserId = user.Id,
                        UserName = user.UserName
                    };
                    playerRepository.CreatePlayer(player);
                    playerRepository.Save();
                }
            }

            username = await userManager.FindByNameAsync("daf");
            if (username == null)
            {
                User user = new User
                {
                    UserName = "daf",
                    Email = "daf@daf.com",
                    Gender = Gender.Male,
                    RegistrationDate = DateTime.Now,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "asdasd");
                if (result.Succeeded)
                {
                    Player player = new Player
                    {
                        UserId = user.Id,
                        UserName = user.UserName
                    };
                    playerRepository.CreatePlayer(player);
                    playerRepository.Save();
                }
            }

            username = await userManager.FindByNameAsync("miki");
            if (username == null)
            {
                User user = new User
                {
                    UserName = "miki",
                    Email = "miki@miki.com",
                    Gender = Gender.Male,
                    RegistrationDate = DateTime.Now,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "asdasd");
                if (result.Succeeded)
                {
                    Player player = new Player
                    {
                        UserId = user.Id,
                        UserName = user.UserName
                    };
                    playerRepository.CreatePlayer(player);
                    playerRepository.Save();
                }
            }

            username = await userManager.FindByNameAsync("viz");
            if (username == null)
            {
                User user = new User
                {
                    UserName = "viz",
                    Email = "viz@viz.com",
                    Gender = Gender.Male,
                    RegistrationDate = DateTime.Now,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "asdasd");
                if (result.Succeeded)
                {
                    Player player = new Player
                    {
                        UserId = user.Id,
                        UserName = user.UserName
                    };
                    playerRepository.CreatePlayer(player);
                    playerRepository.Save();
                }
            }
        }
    }
}
