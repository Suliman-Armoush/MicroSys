using Application.DTOs.Response;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Features.Department.Command.Update
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, DepartmentResponseDto>
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public UpdateDepartmentCommandHandler(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }

        public async Task<DepartmentResponseDto> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _departmentService.GetByIdAsync(request.Id);

            if (department == null)
            return null;

            _mapper.Map(request.DepartmentDto, department);

            await _departmentService.UpdateAsync(department);

            return _mapper.Map<DepartmentResponseDto>(department);
        }
    }
}