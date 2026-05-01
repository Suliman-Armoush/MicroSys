using Application.Features.Department.Command.Update;
using FluentValidation;

namespace Application.Features.Department.Command.Update
{
    public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentCommandValidator()
        {
            
            RuleFor(x => x.DepartmentDto.Name)
                .MaximumLength(100).WithMessage("The department name cannot exceed 100 characters.");
        }
    }
}