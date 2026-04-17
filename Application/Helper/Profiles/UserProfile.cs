using Application.DTOs.Request;
using Application.DTOs.Response;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Helper.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponseDto>().ReverseMap();
            CreateMap<UserRequestDto, User>().ReverseMap();
            CreateMap<UpdateUserRequestDto, User>();
        }
    }
}
