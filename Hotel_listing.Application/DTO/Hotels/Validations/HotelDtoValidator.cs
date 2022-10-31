using FluentValidation;

namespace Hotel_listing.Application.DTO.Hotels.Validations;

public class HotelDtoValidator:AbstractValidator<HotelDto>
{
    public HotelDtoValidator()
    {
        RuleFor(h => h.Name);
    }
}