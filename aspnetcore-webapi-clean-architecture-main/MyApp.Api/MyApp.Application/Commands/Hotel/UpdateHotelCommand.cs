using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using MyApp.Application.DTOs.Hotel;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;

namespace MyApp.Application.Commands.Hotel
{
    public record UpdateHotelCommand(int HotelId, UpdateHotelRequest HotelRequest) : IRequest<HotelDto>;

    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, HotelDto>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IValidator<UpdateHotelCommand> _validator;
        private readonly IMapper _mapper;

        public UpdateHotelCommandHandler(
            IHotelRepository hotelRepository,
            IValidator<UpdateHotelCommand> validator,
            IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<HotelDto> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
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

            if (request.HotelRequest.Rating.HasValue &&
                (request.HotelRequest.Rating < 0 || request.HotelRequest.Rating > 5))
            {
                throw new ArgumentException("Rating must be between 0 and 5.");
            }

            if (!string.IsNullOrEmpty(request.HotelRequest.Name))
                existingHotel.Name = request.HotelRequest.Name;

            if (request.HotelRequest.Rating.HasValue)
                existingHotel.Rating = request.HotelRequest.Rating.Value;

            if (!string.IsNullOrEmpty(request.HotelRequest.Country))
                existingHotel.Country = request.HotelRequest.Country;

            if (!string.IsNullOrEmpty(request.HotelRequest.City))
                existingHotel.City = request.HotelRequest.City;

            if (!string.IsNullOrEmpty(request.HotelRequest.Address))
                existingHotel.Address = request.HotelRequest.Address;

            var updatedHotel = await _hotelRepository.UpdateHotelAsync(request.HotelId, existingHotel);

            return _mapper.Map<HotelDto>(updatedHotel);
        }
    }
}