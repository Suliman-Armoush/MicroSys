using Application.DTOs.Response;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Queries.GetAll
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserResponseDto>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<List<UserResponseDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllAsync();

            if (users == null)
                throw new KeyNotFoundException("No users found.");

            return _mapper.Map<List<UserResponseDto>>(users);
        }
    }
}
