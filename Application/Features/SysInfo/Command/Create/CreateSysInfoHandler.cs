using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Application.Interfaces;

namespace Application.Features.SysInfo.Command.Create
{
    

    public class CreateSysInfoHandler : IRequestHandler<CreateSysInfoCommand, int>
    {
        private readonly ISysInfoService _service;

        public CreateSysInfoHandler(ISysInfoService service)
        {
            _service = service;
        }

        public async Task<int> Handle(CreateSysInfoCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreateAsync(request.Dto);
        }
    }
}
