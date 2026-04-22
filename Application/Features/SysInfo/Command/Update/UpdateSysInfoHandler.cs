using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.SysInfo.Command.Update
{
    public class UpdateSysInfoHandler : IRequestHandler<UpdateSysInfoCommand, bool>
    {
        private readonly ISysInfoService _service;

        public UpdateSysInfoHandler(ISysInfoService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateSysInfoCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateAsync(request.Id, request.Dto);
        }
    }
}
