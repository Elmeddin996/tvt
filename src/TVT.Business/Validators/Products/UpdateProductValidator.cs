using FluentValidation;
using TVT.Business.DTOs.Products;

namespace TVT.Business.Validators.Products;

public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.CategoryId)
            .GreaterThan(0);

        RuleFor(x => x.BrandId)
            .GreaterThan(0);

        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Model)
            .MaximumLength(200);

        RuleFor(x => x.NameAz)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.NameEn)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.NameRu)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.DescriptionAz)
            .MaximumLength(4000);

        RuleFor(x => x.DescriptionEn)
            .MaximumLength(4000);

        RuleFor(x => x.DescriptionRu)
            .MaximumLength(4000);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.OldPrice)
            .GreaterThanOrEqualTo(0)
            .When(x => x.OldPrice.HasValue);

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.SeoTitleAz)
            .MaximumLength(255);

        RuleFor(x => x.SeoTitleEn)
            .MaximumLength(255);

        RuleFor(x => x.SeoTitleRu)
            .MaximumLength(255);

        RuleFor(x => x.SeoDescriptionAz)
            .MaximumLength(500);

        RuleFor(x => x.SeoDescriptionEn)
            .MaximumLength(500);

        RuleFor(x => x.SeoDescriptionRu)
            .MaximumLength(500);

        RuleFor(x => x.SeoKeywordsAz)
            .MaximumLength(1000);

        RuleFor(x => x.SeoKeywordsEn)
            .MaximumLength(1000);

        RuleFor(x => x.SeoKeywordsRu)
            .MaximumLength(1000);
    }
}
