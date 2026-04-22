using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Application.Features.SysInfo.Command.Create
{
    

    public class CreateSysInfoValidator : AbstractValidator<CreateSysInfoCommand>
    {
        public CreateSysInfoValidator()
        {
            RuleFor(x => x.Dto.MikroTikIp).NotEmpty();
            RuleFor(x => x.Dto.Username).NotEmpty();
            RuleFor(x => x.Dto.Password).NotEmpty();
        }
    }
}
