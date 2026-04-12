using Application.DTOs.Responce;
using Domain.Entities;
using AutoMapper;

namespace Application.Helper
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<User, UserDto>().ReverseMap();
    }
  }
}
