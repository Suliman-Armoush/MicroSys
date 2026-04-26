using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Command.Delete
{
    public class DeleteMikrotikUserValidator : AbstractValidator<DeleteMikrotikUserCommand>
    {
        private readonly IMikrotikService _mikrotikService;

        public DeleteMikrotikUserValidator(IMikrotikService mikrotikService)
        {
            _mikrotikService = mikrotikService;

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MustAsync(async (username, cancellationToken) =>
                {
                    return await _mikrotikService.IsUserExistsAsync(username);
                })
                .WithMessage(x => $"The user '{x.Username}' does not exist on Mikrotik.");
        }
    }
}
