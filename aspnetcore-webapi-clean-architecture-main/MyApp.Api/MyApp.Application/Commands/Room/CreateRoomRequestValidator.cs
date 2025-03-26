using FluentValidation;
using MyApp.Application.DTOs.Room;
using MyApp.Core.Interfaces;



    public class CreateRoomRequestValidator : AbstractValidator<CreateRoomRequest>
    {
        public CreateRoomRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.HotelId).GreaterThan(0)
                .WithMessage("Hotel ID must be valid");
        }
    }
