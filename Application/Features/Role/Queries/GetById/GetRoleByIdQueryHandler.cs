using Application.DTOs.Response;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Role.Queries.GetById
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleResponseDto>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public GetRoleByIdQueryHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task<RoleResponseDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleService.GetByIdAsync(request.Id);

            if (role == null) return null;

            return _mapper.Map<RoleResponseDto>(role);
        }
    }
}