using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.SysInfo.Command.Update
{
    public class UpdateSysInfoValidator : AbstractValidator<UpdateSysInfoCommand>
    {
        public UpdateSysInfoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Dto.MikroTikIp).NotEmpty();
        }
    }
}
