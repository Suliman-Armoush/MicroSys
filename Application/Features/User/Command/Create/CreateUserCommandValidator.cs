using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Command.Create
{

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IDepartmentService _departmentService;

        public CreateUserCommandValidator(IUserService userService, IRoleService roleService, IDepartmentService departmentService)
        {
            _userService = userService;
            _roleService = roleService;
            _departmentService = departmentService;

            RuleFor(x => x.UserDto.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.UserDto.Email)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("A valid email address is required.")
                .MustAsync(async (email, _) => await _userService.IsEmailUniqueAsync(email))
                .WithMessage("This email is already registered.");

            RuleFor(x => x.UserDto.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

            RuleFor(x => x.UserDto.RoleId)
                .NotEmpty().WithMessage("Role selection is required.")
                .MustAsync(async (roleId, _) =>
                {
                    var role = await _roleService.GetByIdAsync(roleId);
                    return role != null;
                })
                .WithMessage("The selected Role does not exist.");

            RuleFor(x => x.UserDto.DepartmentId)
                .NotEmpty().WithMessage("Department selection is required.")
                .MustAsync(async (deptId, _) =>
                {
                    var dept = await _departmentService.GetByIdAsync(deptId);
                    return dept != null;
                })
                .WithMessage("The selected Department does not exist.");
        }
    }
}


