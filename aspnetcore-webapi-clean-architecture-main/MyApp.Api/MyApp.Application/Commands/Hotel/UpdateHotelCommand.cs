using AutoMapper;
using MediatR;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;
using MyApp.Application.DTOs.Hotel;

namespace MyApp.Application.Commands.Hotel
{
    public record UpdateHotelCommand(int HotelId, UpdateHotelRequest HotelRequest) : IRequest<HotelEntity>;

    public class UpdateHotelCommandHandler(IHotelRepository hotelRepository, IMapper mapper)
        : IRequestHandler<UpdateHotelCommand, HotelEntity>
    {
        public async Task<HotelEntity> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            var hotelEntity = mapper.Map<HotelEntity>(request.HotelRequest);
            return await hotelRepository.UpdateHotelAsync(request.HotelId, hotelEntity);
        }
    }
}