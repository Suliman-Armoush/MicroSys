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
            return await _mikrotikService.DeleteUserAsync(request.Username);
        }
    }
}
