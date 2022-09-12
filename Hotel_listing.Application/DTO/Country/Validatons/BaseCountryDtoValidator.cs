using FluentValidation;
using Hotel_listing.Application.DTO.Country;

namespace Hotel_listing.Application.Validation.Country;

public class BaseCountryDtoValidator:AbstractValidator<BaseCountryDto>
{
    public BaseCountryDtoValidator()
    { }
}