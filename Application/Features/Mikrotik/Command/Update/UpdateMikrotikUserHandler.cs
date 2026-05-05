using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Mikrotik.Command.Update
{
    public class UpdateMikrotikUserHandler : IRequestHandler<UpdateMikrotikUserCommand, MikrotikUserInformationResponse>
    {
        private readonly IMikrotikService _mikrotikService;
        private readonly IDepartmentService _departmentService;

        public UpdateMikrotikUserHandler(IMikrotikService mikrotikService, IDepartmentService departmentService)
        {
            _mikrotikService = mikrotikService;
            _departmentService = departmentService;
        }

        public async Task<MikrotikUserInformationResponse> Handle(UpdateMikrotikUserCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _mikrotikService.GetUserByNameAsync(request.CurrentUsername);
            if (currentUser == null) throw new KeyNotFoundException("User not found on Mikrotik.");

            string oldDeptName = "";
            string oldDetails = "";
            if (!string.IsNullOrEmpty(currentUser.Comment) && currentUser.Comment.Contains("-"))
            {
                var parts = currentUser.Comment.Split('-');
                oldDeptName = parts[0].Trim();
                oldDetails = parts[1].Trim();
            }
            else
            {
                oldDetails = currentUser.Username;
            }

            string finalDeptName = oldDeptName;
            if (request.DepartmentId.HasValue && request.DepartmentId > 0)
            {
                var department = await _departmentService.GetByIdAsync(request.DepartmentId.Value);
                if (department != null) finalDeptName = department.Name;
            }

            string finalDetails = !string.IsNullOrEmpty(request.UserDetails) ? request.UserDetails : oldDetails;

            string finalComment = $"{finalDeptName} - {finalDetails}";

            string limitValue = null;
            if (request.LimitGB.HasValue && request.LimitGB.Value > 0)
            {
                limitValue = $"{request.LimitGB.Value}G"; 
            }
            else if (currentUser.LimitGB > 0)
            {
                limitValue = $"{currentUser.LimitGB}G";
            }

            var serviceRequest = new UpdateMikrotikUserRequest
            {
                NewUsername = request.NewUsername,
                Password = request.Password,
                Profile = request.Profile,
                Server = request.Server,
                Comment = finalComment,
                LimitBytes = limitValue 
            };

            return await _mikrotikService.UpdateUserAsync(serviceRequest, request.CurrentUsername);
        }
    }
}
