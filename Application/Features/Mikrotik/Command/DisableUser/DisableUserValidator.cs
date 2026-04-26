using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Command.DisableUser
{
    public class DisableUserValidator : AbstractValidator<DisableMikrotikUserCommand>
    {
        private readonly IMikrotikService _mikrotikService;

        public DisableUserValidator(IMikrotikService mikrotikService)
        {
            _mikrotikService = mikrotikService;

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.");

            RuleFor(x => x.Username)
                .MustAsync(async (username, cancellationToken) =>
                {
                    return await _mikrotikService.IsUserExistsAsync(username);
                })
                .WithMessage(x => $"The user '{x.Username}' does not exist on Mikrotik.");

            RuleFor(x => x.Username)
                .MustAsync(async (username, cancellationToken) =>
                {
                    var user = await _mikrotikService.GetUserByNameAsync(username);

                    
                    return user != null && user.IsDisabled == false;
                })
                .WithMessage("This user is already disabled.");
        }
    }

}
