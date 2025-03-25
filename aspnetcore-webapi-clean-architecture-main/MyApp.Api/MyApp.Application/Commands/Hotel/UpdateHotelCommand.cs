using AutoMapper;
using MediatR;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;
using MyApp.Application.DTOs.Hotel;

namespace MyApp.Application.Commands.Hotel
{
    public record UpdateHotelCommand(int HotelId, HotelEntity Hotel) : IRequest<HotelEntity>;

    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, HotelEntity>
    {
        private readonly IHotelRepository _hotelRepository;

        public UpdateHotelCommandHandler(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<HotelEntity> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            return await _hotelRepository.UpdateHotelAsync(request.HotelId, request.Hotel);
        }
    }
}