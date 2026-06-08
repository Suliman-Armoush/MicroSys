using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Features.Mikrotik.Command.Create;
using Application.Interfaces;
using MediatR;
using Microsoft.OpenApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Command.AddUser
{
  public class AddMikrotikUserHandler : IRequestHandler<AddMikrotikUserCommand, MikrotikUserInformationResponse>
  {
    private readonly IMikrotikService _mikrotikService;
    private readonly IDepartmentService _departmentService;
    private readonly IUserService _userService;

    public AddMikrotikUserHandler(IMikrotikService mikrotikService, IDepartmentService departmentService, IUserService userService)
    {
      _mikrotikService = mikrotikService;
      _departmentService = departmentService;
      _userService = userService;
    }

    public async Task<MikrotikUserInformationResponse> Handle(AddMikrotikUserCommand request, CancellationToken cancellationToken)
    {
      var user = await _userService.GetByIdAsync(_userService.UserId);
      if (!user.CreatePerm)
      {
        throw new UnauthorizedAccessException("User does not have permission to create a new Mikrotik user");
      }

      var department = await _departmentService.GetByIdAsync(user.Department.Id);
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
        Server = "all",
        Comment = finalComment,
        LimitBytes = limitValue
      };

      // 1. إنشاء المستخدم
      var result = await _mikrotikService.CreateUserAsync(serviceRequest);

      // 2. إذا نجح الإنشاء ووجد MacAddress، قم بحذف الـ Host
      if (result != null && !string.IsNullOrEmpty(request.MacAddress))
      {
        try
        {
          var removed = await _mikrotikService.RemoveHostByMacAsync(request.MacAddress);

          // يمكنك تسجيل النتيجة في اللوج إذا أردت
          if (removed)
          {
            Console.WriteLine($"Host with MAC {request.MacAddress} removed successfully after user creation.");
          }
          else
          {
            Console.WriteLine($"Host with MAC {request.MacAddress} not found or could not be removed.");
          }
        }
        catch (Exception ex)
        {
          // لا نريد أن تفشل عملية إنشاء المستخدم إذا فشل حذف الـ Host
          Console.WriteLine($"Error removing host after user creation: {ex.Message}");
        }
      }

      return result;
    }
  }
}