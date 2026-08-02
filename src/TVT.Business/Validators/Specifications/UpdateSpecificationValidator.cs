using FluentValidation;
using TVT.Business.DTOs.Specifications;

namespace TVT.Business.Validators.Specifications;

public class UpdateSpecificationValidator : AbstractValidator<UpdateSpecificationDto>
{
    public UpdateSpecificationValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.SpecificationGroupId)
            .GreaterThan(0);

        RuleFor(x => x.NameAz)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.NameEn)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.NameRu)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.DisplayOrder)
            .GreaterThanOrEqualTo(0);
    }
}
