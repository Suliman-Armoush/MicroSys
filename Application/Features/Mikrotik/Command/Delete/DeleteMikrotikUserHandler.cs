using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Command.Delete
{
    public class DeleteMikrotikUserHandler : IRequestHandler<DeleteMikrotikUserCommand, bool>
    {
        private readonly IMikrotikService _mikrotikService;

        public DeleteMikrotikUserHandler(IMikrotikService mikrotikService)
        {
            _mikrotikService = mikrotikService;
        }

        public async Task<bool> Handle(DeleteMikrotikUserCommand request, CancellationToken cancellationToken)
        {
            // بمجرد وصولنا هنا، الفالديتور تأكد تماماً أن اليوزر موجود
            var result = await _mikrotikService.DeleteUserAsync(request.Username);
            if (result != null && !string.IsNullOrEmpty(request.Username))
            {
                try
                {
                    var removed = await _mikrotikService.RemoveHostByMacAsync(request.Username);

                    // يمكنك تسجيل النتيجة في اللوج إذا أردت
                    if (removed)
                    {
                        Console.WriteLine($"Host with MAC {request.Username} removed successfully after user creation.");
                    }
                    else
                    {
                        Console.WriteLine($"Host with MAC {request.Username} not found or could not be removed.");
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
