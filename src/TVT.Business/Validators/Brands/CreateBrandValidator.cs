using FluentValidation;
using TVT.Business.DTOs.Brands;

namespace TVT.Business.Validators.Brands;

public sealed class CreateBrandValidator : AbstractValidator<CreateBrandDto>
{
    public CreateBrandValidator()
    {
        RuleFor(x => x.NameAz)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.NameEn)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.NameRu)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.SlugAz)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(x => x.SlugEn)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(x => x.SlugRu)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(x => x.DescriptionAz)
            .MaximumLength(2000);

        RuleFor(x => x.DescriptionEn)
            .MaximumLength(2000);

        RuleFor(x => x.DescriptionRu)
            .MaximumLength(2000);

        RuleFor(x => x.Website)
            .MaximumLength(500)
            .Must(url =>
                string.IsNullOrWhiteSpace(url) ||
                Uri.TryCreate(url, UriKind.Absolute, out _))
            .WithMessage("Please enter a valid website URL.");
    }
}
