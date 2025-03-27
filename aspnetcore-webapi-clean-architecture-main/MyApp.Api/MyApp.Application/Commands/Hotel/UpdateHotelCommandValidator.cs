using FluentValidation;
using MyApp.Application.Commands.Hotel;

public class UpdateHotelCommandValidator : AbstractValidator<UpdateHotelCommand>
{
    public UpdateHotelCommandValidator()
    {
        RuleFor(command => command.HotelRequest.Rating)
            .InclusiveBetween(0, 5)
            .WithMessage("Rating must be between 0 and 5.");

    }
}
