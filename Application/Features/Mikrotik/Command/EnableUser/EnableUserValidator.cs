using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Command.EnableUser
{
    public class EnableUserValidator : AbstractValidator<EnableMikrotikUserCommand>
    {
        private readonly IMikrotikService _mikrotikService;

        public EnableUserValidator(IMikrotikService mikrotikService)
        {
            _mikrotikService = mikrotikService;

            // 1. التأكد أن حقل الاسم ليس فارغاً
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.");

            // 2. التأكد أن المستخدم موجود في المايكروتك
            RuleFor(x => x.Username)
                .MustAsync(async (username, token) => await _mikrotikService.IsUserExistsAsync(username))
                .WithMessage(x => $"The user '{x.Username}' does not exist on Mikrotik.");

            // 3. المنطق: لا يمكن تفعيل يوزر إلا إذا كان معطلاً (IsDisabled == true)
            RuleFor(x => x.Username)
                .MustAsync(async (username, token) =>
                {
                    var user = await _mikrotikService.GetUserByNameAsync(username);
                    // نسمح بالمرور فقط إذا كان المستخدم معطلاً حالياً
                    return user != null && user.IsDisabled == true;
                })
                .WithMessage("This user is already enabled.");
        }
    }
}
