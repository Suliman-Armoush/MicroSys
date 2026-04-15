using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Role.Queries.GetAll
{
    public record GetAllRolesQuery() : IRequest<List<RoleResponseDto>>;
}
