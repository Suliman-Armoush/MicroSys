using Application.DTOs.Request;
using Application.DTOs.Response;
using MediatR;


public record UpdateRoleCommand(int Id, RoleRequestDto RoleDto) : IRequest<RoleResponseDto>;