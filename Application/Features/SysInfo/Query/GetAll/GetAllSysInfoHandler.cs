using Application.DTOs.Response;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.SysInfo.Query.GetAll
{
    public class GetAllSysInfoHandler : IRequestHandler<GetAllSysInfoQuery, IEnumerable<SysInfoResponseDto>>
    {
        private readonly ISysInfoService _service;

        public GetAllSysInfoHandler(ISysInfoService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<SysInfoResponseDto>> Handle(GetAllSysInfoQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllAsync();
        }
    }
}
