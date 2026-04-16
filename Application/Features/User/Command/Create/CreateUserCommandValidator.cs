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

        public CreateUserCommandValidator(IUserService userService)
        {
            _userService = userService;

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
                .NotEmpty().WithMessage("Role selection is required.");

            RuleFor(x => x.UserDto.DepartmentId)
                .NotEmpty().WithMessage("Department selection is required.");
        }
    }
}


