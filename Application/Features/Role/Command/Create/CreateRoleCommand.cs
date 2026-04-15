using Application.DTOs.Request;
using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Role.Command.Create
{
    public record CreateRoleCommand(RoleRequestDto RoleDto) : IRequest<RoleResponseDto>;
}
