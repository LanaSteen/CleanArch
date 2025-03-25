using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;

namespace MyApp.Application.Commands.Hotel
{
    public record UpdateHotelCommand(int HotelId, HotelEntity Hotel) : IRequest<HotelEntity>;

    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, HotelEntity>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IValidator<UpdateHotelCommand> _validator;

        public UpdateHotelCommandHandler(IHotelRepository hotelRepository, IValidator<UpdateHotelCommand> validator)
        {
            _hotelRepository = hotelRepository;
            _validator = validator;
        }

        public async Task<HotelEntity> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingHotel = await _hotelRepository.GetHotelByIdAsync(request.HotelId);
            if (existingHotel == null)
            {
                throw new KeyNotFoundException("Hotel not found");
            }

            if (request.Hotel.Rating < 0 || request.Hotel.Rating > 5)
            {
                throw new ArgumentException("Rating must be between 0 and 5.");
            }
            if (!string.IsNullOrEmpty(request.Hotel.Name))
            {
                existingHotel.Name = request.Hotel.Name;
            }

            if (!string.IsNullOrEmpty(request.Hotel.City))
            {
                existingHotel.City = request.Hotel.City;
            }

            if (!string.IsNullOrEmpty(request.Hotel.Address))
            {
                existingHotel.Address = request.Hotel.Address;
            }

            if (request.Hotel.Rating != default(int))
            {
                existingHotel.Rating = request.Hotel.Rating;
            }

            if (request.Hotel.ManagerId != default(int))
            {
                existingHotel.ManagerId = request.Hotel.ManagerId;
            }

            return await _hotelRepository.UpdateHotelAsync(request.HotelId, existingHotel);
        }
    }
}
