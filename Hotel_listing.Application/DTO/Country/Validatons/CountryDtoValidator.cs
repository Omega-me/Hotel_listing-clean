using FluentValidation;
using Hotel_listing.Application.DTO.Country;

namespace Hotel_listing.Application.Validation.Country;

public class CountryDtoValidator : AbstractValidator<CountryDto>
{
    public CountryDtoValidator()
    {
        RuleFor(c=>c.Name)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .NotNull();
        RuleFor(c=>c.ShortName)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .Length(2,2)
            .NotNull();
        RuleFor(c=>c.Id)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .NotNull();
    }
}