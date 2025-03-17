using FluentValidation;
using MyApp.Application.Commands.Hotel;
using MyApp.Core.Entities;

namespace MyApp.Application.Validators
{
    public class CreateHotelCommandValidator : AbstractValidator<CreateHotelCommand>
    {
        public CreateHotelCommandValidator()
        {
            RuleFor(x => x.Hotel.Name)
                .NotEmpty().WithMessage("Hotel name is required.")
                .MaximumLength(100).WithMessage("Hotel name cannot exceed 100 characters.");

            RuleFor(x => x.Hotel.Rating)
                .InclusiveBetween(0, 5).WithMessage("Rating must be between 0 and 5.");

            RuleFor(x => x.Hotel.Country)
                .NotEmpty().WithMessage("Country is required.");

            RuleFor(x => x.Hotel.City)
                .NotEmpty().WithMessage("City is required.");

            RuleFor(x => x.Hotel.Address)
                .NotEmpty().WithMessage("Address is required.");
        }
    }
}
