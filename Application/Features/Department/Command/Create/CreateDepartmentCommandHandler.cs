using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Features.Department.Command.Create
{
  public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, DepartmentResponseDto>
  {
    private readonly IDepartmentService _departmentService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public CreateDepartmentCommandHandler(IDepartmentService departmentService, IMapper mapper, IUserService userService)
    {
      _departmentService = departmentService;
      _userService = userService;
      _mapper = mapper;
    }

    public async Task<DepartmentResponseDto> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
      var departmentDto = _mapper.Map<Domain.Entities.Department>(request.DepartmentDto);
      var department = await _departmentService.AddDep(departmentDto);

      var userDto = new UserRequestDto
      {
        Name = department.Name + " manager",
        UserName = department.Name,
        Password = "12345678",
        RoleId = 2,
        DepartmentId = department.Id,
        CreatePerm = false,
        ChangePerm = true,
        UpdatePerm = false,
      };

      var user = _mapper.Map<Domain.Entities.User>(userDto);
      user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

      await _userService.AddAsync(user);

      return _mapper.Map<DepartmentResponseDto>(department);
    }
  }
}