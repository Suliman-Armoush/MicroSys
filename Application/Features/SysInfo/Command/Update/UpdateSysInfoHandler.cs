using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.SysInfo.Command.Update
{
  public class UpdateSysInfoHandler : IRequestHandler<UpdateSysInfoCommand, bool>
  {
    private readonly ISysInfoService _service;
    private readonly IMapper _mapper;


    public UpdateSysInfoHandler(ISysInfoService service, IMapper mapper)
    {
      _service = service;
      _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateSysInfoCommand request, CancellationToken cancellationToken)
    {
      //return await _service.UpdateAsync(request.Id, request.Dto);

      var sysInfo = await _service.GetByIdAsync(request.Id);

      if (sysInfo == null)
        throw new KeyNotFoundException("Department not found.");

      _mapper.Map(request.Dto, sysInfo);

      return await _service.UpdateAsync(sysInfo);
    }
  }
}
