using Application.DTOs.Request;
using Application.DTOs.Response;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Helper.Profiles
{
    public class SysInfoProfile : Profile
    {
        public SysInfoProfile()
        {
            CreateMap<SysInfo, SysInfoResponseDto>();

            CreateMap<SysInfoRequestDto, SysInfo>();
        }
    }
}
