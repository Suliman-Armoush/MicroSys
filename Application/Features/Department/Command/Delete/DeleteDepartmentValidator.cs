using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Department.Command.Delete
{
    public class DeleteDepartmentValidator : AbstractValidator<DeleteDepartmentCommand>
    {
        public DeleteDepartmentValidator(IDepartmentService departmentService)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .MustAsync(async (id, cancellation) =>
                {
                    bool hasUsers = await departmentService.HasUsersAsync(id);
                    return !hasUsers;
                })
                .WithMessage("Cannot delete department because it contains active users. Please reassign or delete the users first.");
        }
    }
}
