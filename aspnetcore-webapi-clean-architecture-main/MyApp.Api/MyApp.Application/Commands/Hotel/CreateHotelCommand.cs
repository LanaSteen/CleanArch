using MediatR;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace MyApp.Application.Commands.Hotel
{
    public record CreateHotelCommand(HotelEntity Hotel) : IRequest<HotelEntity>;

    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, HotelEntity>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IValidator<CreateHotelCommand> _validator;

        public CreateHotelCommandHandler(IHotelRepository hotelRepository, IValidator<CreateHotelCommand> validator)
        {
            _hotelRepository = hotelRepository;
            _validator = validator;
        }

        public async Task<HotelEntity> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return await _hotelRepository.AddHotelAsync(request.Hotel);
        }
    }
}
