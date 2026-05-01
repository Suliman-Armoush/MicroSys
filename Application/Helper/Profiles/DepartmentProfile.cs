using Application.DTOs.Request;
using Application.DTOs.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Application.Helper.Profiles
{
  public class DepartmentProfile : Profile
  {

    public DepartmentProfile()
    {

      CreateMap<Department, DepartmentResponseDto>().ReverseMap()
      .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));


      CreateMap<DepartmentRequestDto, Department>()
    .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }


  }
}
