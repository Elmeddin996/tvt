using FluentValidation;
using TVT.Business.DTOs.Settings;

namespace TVT.Business.Validators.Settings;

public sealed class UpdateSettingValidator : AbstractValidator<UpdateSettingDto>
{
    public UpdateSettingValidator()
    {
        RuleFor(x => x.CompanyName)
            .NotEmpty();

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email));
    }
}