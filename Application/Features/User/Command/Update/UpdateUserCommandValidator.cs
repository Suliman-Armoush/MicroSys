using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Command.Update
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator(IUserService userService, IRoleService roleService, IDepartmentService departmentService)
        {
            
            RuleFor(x => x.UserDto.Email)
                 .EmailAddress().WithMessage("A valid email address is required.")
                 .MustAsync(async (command, email, cancellationToken) => 
                     await userService.IsEmailUniqueAsync(email!, command.Id))
                    .When(x => !string.IsNullOrEmpty(x.UserDto.Email))
                     .WithMessage("This email is already registered to another user.");
          
            RuleFor(x => x.UserDto.RoleId)
                .MustAsync(async (roleId, _) => (await roleService.GetByIdAsync(roleId!.Value)) != null)
                .When(x => x.UserDto.RoleId.HasValue)
                .WithMessage("The selected role does not exist.");

            RuleFor(x => x.UserDto.DepartmentId)
                .MustAsync(async (deptId, _) => (await departmentService.GetByIdAsync(deptId!.Value)) != null)
                .When(x => x.UserDto.DepartmentId.HasValue)
                .WithMessage("The selected department does not exist.");

            RuleFor(x => x.UserDto.Password)
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .When(x => !string.IsNullOrEmpty(x.UserDto.Password));
        }
    }
}
