using Application.DTOs.Response;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.SysInfo.Query.GetById
{
    public class GetSysInfoByIdHandler : IRequestHandler<GetSysInfoByIdQuery, SysInfoResponseDto?>
    {
        private readonly ISysInfoService _service;
    private readonly IMapper _mapper;


    public GetSysInfoByIdHandler(ISysInfoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<SysInfoResponseDto?> Handle(GetSysInfoByIdQuery request, CancellationToken cancellationToken)
        {
      // return await _service.GetByIdAsync(request.Id);

      var sysInfo = await _service.GetByIdAsync(request.Id);

      if (sysInfo == null)
        throw new KeyNotFoundException("sysInfo not found.");

      return _mapper.Map<SysInfoResponseDto>(sysInfo);
    }
    }
}
