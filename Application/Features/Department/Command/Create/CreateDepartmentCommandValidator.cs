using Application.Features.Department.Command.Create;
using FluentValidation;

namespace Application.Features.Department.Command.Create
{
    public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentCommandValidator() =>
            
            RuleFor(x => x.DepartmentDto.Name)
            .NotEmpty().WithMessage("The department name cannot be empty.")
            .MaximumLength(100).WithMessage("The department name cannot exceed 100 characters.");
    }
}