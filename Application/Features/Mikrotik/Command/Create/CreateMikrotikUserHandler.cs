using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Command.Create
{
    public class CreateMikrotikUserHandler : IRequestHandler<CreateMikrotikUserCommand, MikrotikUserInformationResponse>
    {
        private readonly IMikrotikService _mikrotikService;
        private readonly IDepartmentService _departmentService;

        public CreateMikrotikUserHandler(IMikrotikService mikrotikService, IDepartmentService departmentService)
        {
            _mikrotikService = mikrotikService;
            _departmentService = departmentService;
        }

        public async Task<MikrotikUserInformationResponse> Handle(CreateMikrotikUserCommand request, CancellationToken cancellationToken)
        {
            var department = await _departmentService.GetByIdAsync(request.DepartmentId);
            if (department == null)
                throw new KeyNotFoundException("Department not found.");

            string finalComment = $"{department.Name} - {request.UserDetails ?? request.Username}";

            // تحويل الرقم إلى صيغة ميكروتك "10G"
            string limitValue = (request.LimitGB.HasValue && request.LimitGB.Value > 0)
                                ? $"{request.LimitGB.Value}G"
                                : null;

            var serviceRequest = new CreateMikrotikUserRequest
            {
                Username = request.Username,
                Password = request.Password,
                Profile = request.Profile,
                Server = request.Server,
                Comment = finalComment,
                LimitBytes = limitValue
            };

            return await _mikrotikService.CreateUserAsync(serviceRequest);
        }
    }
}
