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
          
            CreateMap<Department, DepartmentResponseDto>().ReverseMap();
            CreateMap<DepartmentRequestDto, Department>().ReverseMap();
        }


    }
}
