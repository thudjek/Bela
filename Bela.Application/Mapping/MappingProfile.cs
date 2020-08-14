using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Bela.Application.ViewModels.Room;
using Bela.Application.ViewModels.User;
using Bela.Domain.Entities;
using Bela.Domain.Enums;
using Bela.Domain.Extensions;

namespace Bela.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterModel, User>();
            CreateMap<User, UserListViewModel>();
            CreateMap<User, UserInRoomViewModel>();
            CreateMap<User, UserDetailsModel>()
                .ForMember(vm => vm.Gender, opt => opt.MapFrom(r => r.Gender.GetDisplayName()))
                .ForMember(vm => vm.RegistrationDate, opt => opt.MapFrom(r => r.RegistrationDate.ToShortDateString()));

            CreateMap<CreateRoomModel, Room>();
            CreateMap<Room, RoomViewModel>();
            CreateMap<Room, RoomListViewModel>()
                .ForMember(vm => vm.PlayerCount, opt => opt.MapFrom(r => r.Users.Count));
        }
    }
}
