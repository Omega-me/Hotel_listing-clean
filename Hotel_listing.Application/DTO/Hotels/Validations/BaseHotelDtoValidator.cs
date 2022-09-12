using FluentValidation;
using Hotel_listing.Application.DTO.Hotels;

namespace Hotel_listing.Application.Validation.Hotel;

public class BaseHotelDtoValidator:AbstractValidator<BaseHotelDto>
{
    public BaseHotelDtoValidator()
    {
        
    }
}