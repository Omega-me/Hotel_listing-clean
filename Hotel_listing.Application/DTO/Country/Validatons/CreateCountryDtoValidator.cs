using FluentValidation;

namespace Hotel_listing.Application.DTO.Country.Validatons;

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