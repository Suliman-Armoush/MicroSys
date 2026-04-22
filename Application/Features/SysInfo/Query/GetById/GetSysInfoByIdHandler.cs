using Application.DTOs.Response;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.SysInfo.Query.GetById
{
    public class GetSysInfoByIdHandler : IRequestHandler<GetSysInfoByIdQuery, SysInfoResponseDto?>
    {
        private readonly ISysInfoService _service;

        public GetSysInfoByIdHandler(ISysInfoService service)
        {
            _service = service;
        }

        public async Task<SysInfoResponseDto?> Handle(GetSysInfoByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetByIdAsync(request.Id);
        }
    }
}
