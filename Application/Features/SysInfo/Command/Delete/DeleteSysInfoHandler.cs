using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.SysInfo.Command.Delete
{
    public class DeleteSysInfoHandler : IRequestHandler<DeleteSysInfoCommand, bool>
    {
        private readonly ISysInfoService _service;

        public DeleteSysInfoHandler(ISysInfoService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteSysInfoCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteAsync(request.Id);
        }
    }
}
