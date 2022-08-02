using FluentValidation;
using Hotel_listing.Application.DTO.Country;

namespace Hotel_listing.Application.Validation.Country;

public class CreateCountryDtoValidator:AbstractValidator<CreateCountryDto>
{
    public CreateCountryDtoValidator()
    {
        RuleFor(c => c.Name)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .NotNull();
        RuleFor(c=>c.ShortName)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .Length(2,2)
            .NotNull();
    }   
}