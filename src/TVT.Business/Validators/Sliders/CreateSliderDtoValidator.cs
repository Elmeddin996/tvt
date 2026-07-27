using FluentValidation;
using TVT.Business.DTOs.Sliders;

namespace TVT.Business.Validators.Sliders;

public class CreateSliderDtoValidator : AbstractValidator<CreateSliderDto>
{
    public CreateSliderDtoValidator()
    {
        RuleFor(x => x.TitleAz)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.TitleEn)
            .MaximumLength(200);

        RuleFor(x => x.TitleRu)
            .MaximumLength(200);

        RuleFor(x => x.DescriptionAz)
            .MaximumLength(2000);

        RuleFor(x => x.DescriptionEn)
            .MaximumLength(2000);

        RuleFor(x => x.DescriptionRu)
            .MaximumLength(2000);

        RuleFor(x => x.ButtonTextAz)
            .MaximumLength(100);

        RuleFor(x => x.ButtonTextEn)
            .MaximumLength(100);

        RuleFor(x => x.ButtonTextRu)
            .MaximumLength(100);

        RuleFor(x => x.ButtonLink)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.ButtonLink));

        RuleFor(x => x.DisplayOrder)
            .GreaterThanOrEqualTo(0);
    }
}
