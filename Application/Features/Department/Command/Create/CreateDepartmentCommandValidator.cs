using Application.Features.Department.Command.Create;
using FluentValidation;

namespace Application.Features.Department.Command.Create
{
    public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentCommandValidator()
        {
            // القاعدة الأولى: الاسم
            RuleFor(x => x.DepartmentDto.Name)
                .NotEmpty().WithMessage("The department name cannot be empty.")
                .MaximumLength(100).WithMessage("The department name cannot exceed 100 characters.");

            // القاعدة الثانية: النوع
            RuleFor(x => x.DepartmentDto.Type)
                .NotEmpty().WithMessage("The department type cannot be empty.")
                .IsInEnum().WithMessage("The department type is invalid.");

            
        }
    }
}