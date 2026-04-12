using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User
{
  public record GetUserQuery(int Id) : IRequest<UserDto>;

}
