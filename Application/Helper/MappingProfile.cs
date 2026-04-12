using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.DTOs.Response;
using AutoMapper;
using Domain.Entities;

namespace Application.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Department, DepartmentResponseDto>();
            CreateMap<DepartmentRequestDto, Department>();
        }
    }
}
