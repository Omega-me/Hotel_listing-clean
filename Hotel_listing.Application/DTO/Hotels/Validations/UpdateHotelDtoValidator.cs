using FluentValidation;
using Hotel_listing.Application.DTO.Country;

namespace Hotel_listing.Application.DTO.Hotels.Validations;

public class UpdateHotelDtoValidator:AbstractValidator<UpdateCountryDto>
{
    public UpdateHotelDtoValidator()
    {
        
    }
}