using FluentValidation;
using Hotel_listing.Application.DTO.Hotels;

namespace Hotel_listing.Application.Validation.Hotel;

public class HotelDtoValidator:AbstractValidator<HotelDto>
{
    public HotelDtoValidator()
    {
        RuleFor(h => h.Name);
    }
}