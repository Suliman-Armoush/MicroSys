using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Command.Delete
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Invalid User ID.");
        }
    }
}
