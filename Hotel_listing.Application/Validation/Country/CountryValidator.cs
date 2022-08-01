using FluentValidation;
using Hotel_listing.Application.DTO.Country;

namespace Hotel_listing.Application.Validation.Country;

public class CountryValidator:AbstractValidator<CreateCountryDTO>
{
    public CountryValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Country should have a name").NotNull();
        RuleFor(c=>c.ShortName).NotEmpty().WithMessage("Country should have a short name").Length(2,2).NotNull();
    }   
}