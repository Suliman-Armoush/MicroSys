using Application.Features.Mikrotik.Command.Create;
using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Command.AddUser
{
    public class AddMikrotikUserValidator : AbstractValidator<AddMikrotikUserCommand>
    {
        private readonly IMikrotikService _mikrotikService;
        public AddMikrotikUserValidator(IMikrotikService mikrotikService)
        {
            _mikrotikService = mikrotikService;
            RuleFor(x => x.Username)
             .NotEmpty().WithMessage("Username is required.")
             .MustAsync(async (username, cancellation) =>
             {
                 return !await _mikrotikService.IsUserExistsAsync(username);
             }).WithMessage("Username already exists in Mikrotik.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");

            RuleFor(x => x.Profile)
                .NotEmpty().WithMessage("Profile must be selected.");

            RuleFor(x => x.LimitGB)
                .GreaterThan(0).When(x => x.LimitGB.HasValue)
                .WithMessage("Limit must be greater than zero.");
        }
    }
}