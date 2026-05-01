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
      CreateMap<SysInfo, SysInfoResponseDto>()
      .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));


      CreateMap<SysInfoRequestDto, SysInfo>()
    .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
  }
}
