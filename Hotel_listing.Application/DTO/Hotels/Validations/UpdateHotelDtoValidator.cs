using FluentValidation;
using Hotel_listing.Application.DTO.Country;

namespace Hotel_listing.Application.Validation.Hotel;

public class UpdateHotelDtoValidator:AbstractValidator<UpdateCountryDto>
{
    public UpdateHotelDtoValidator()
    {
        
    }
}