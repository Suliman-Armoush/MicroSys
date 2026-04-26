using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Command.Update
{
    public class UpdateMikrotikUserValidator : AbstractValidator<UpdateMikrotikUserCommand>
    {
        private readonly IMikrotikService _mikrotikService;

        public UpdateMikrotikUserValidator(IMikrotikService mikrotikService)
        {
            _mikrotikService = mikrotikService;

            RuleFor(x => x.CurrentUsername)
             .NotEmpty().WithMessage("Username in URL is required.")
             .MustAsync(async (name, cancellation) => await mikrotikService.IsUserExistsAsync(name))
             .WithMessage("This user does not exist on the Mikrotik system.");
        }
    }
}

