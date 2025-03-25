using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Commands.Hotel
{
    public class CreateHotelCommandValidator : AbstractValidator<CreateHotelCommand>
    {
        public CreateHotelCommandValidator()
        {
            RuleFor(x => x.Hotel.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.Hotel.City)
                .NotEmpty().WithMessage("City is required.");

            RuleFor(x => x.Hotel.Address)
                .NotEmpty().WithMessage("Address is required.");

            RuleFor(x => x.Hotel.Rating)
                .InclusiveBetween(0, 5).WithMessage("Rating must be between 0 and 5.");

        }
    }
}
