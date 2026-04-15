using Application.DTOs.Response;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Role.Queries.GetAll
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, List<RoleResponseDto>>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public GetAllRolesQueryHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task<List<RoleResponseDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleService.GetAllAsync();

            return _mapper.Map<List<RoleResponseDto>>(roles);
        }
    }
}
