using FluentValidation;
using TVT.Business.DTOs.SpecificationGroups;

namespace TVT.Business.Validators.SpecificationGroups;

public class UpdateSpecificationGroupValidator : AbstractValidator<UpdateSpecificationGroupDto>
{
    public UpdateSpecificationGroupValidator()
    {
        RuleFor(x => x.Id)
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
