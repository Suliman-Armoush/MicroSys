using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Command.DeleteFromHost
{
    public class RemoveMikrotikHostValidator : AbstractValidator<RemoveMikrotikHostCommand>
    {
        public RemoveMikrotikHostValidator()
        {
            RuleFor(x => x.MacAddress)
                .NotEmpty().WithMessage("MAC Address is required.")
                .Matches(@"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$")
                .WithMessage("Invalid MAC Address format. (Example: AA:BB:CC:DD:EE:FF)");
        }
    }
}
