using AutoMapper;
using Bela.Application.Interfaces;
using Bela.Application.Services;
using Bela.Domain.Entities;
using Bela.Domain.Interfaces;
using Bela.Infrastructure.Data.Context;
using Bela.Infrastructure.Data.Repositories;
using Bela.Infrastructure.Data.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Bela.Infrastructure.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<int>>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireLowercase = false;
                o.User.RequireUniqueEmail = true;
                o.SignIn.RequireConfirmedEmail = false;
            })
            .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
            .AddEntityFrameworkStores<BelaDbContext>()
            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(o =>
            {
                o.Cookie.Name = "BelotCookie";
                o.LoginPath = "/Home/Index";
            });

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IRoomService, RoomService>();

            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
        }

        public static void AddDbContext(IServiceCollection services, string connString)
        {
            services.AddDbContext<BelaDbContext>(o => o.UseSqlServer(connString));
        }

        public static void AddMappingProfile(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
