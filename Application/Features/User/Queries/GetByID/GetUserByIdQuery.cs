using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Queries.GetByID
{
    public record GetUserByIdQuery(int Id) : IRequest<UserResponseDto?>;
}
