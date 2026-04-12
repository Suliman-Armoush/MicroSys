//using Application.DTOs.Response;
//using Application.Interfaces;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Application.Features.User
//{
//  public class GetUserHandler : IRequestHandler<GetUserQuery, UserDto>
//  {
//    private readonly IUserService _userService;

//    public GetUserHandler(IUserService userService)
//    {
//      _userService = userService;
//    }

//    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
//    {
//      var user = await _userService.GetByIdAsync(request.Id);

//      if (user == null)
//        return null; // أو ترجع Exception أو Result

//      return new UserDto
//      {
//        Id = user.Id,
//        Name = user.Name,
//        Age = user.Age,
//        Phone = user.Phone
//      };
//    }
//  }
//}
