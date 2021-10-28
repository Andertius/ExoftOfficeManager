using System.Linq;

using AutoMapper;

using ExoftOfficeManager.Domain.Dtos;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.Mappers
{
    public class WorkPlaceMapper
    {
        public static WorkPlaceDto MapIntoDto(WorkPlace source)
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<WorkPlace, WorkPlaceDto>()
                .ForMember(nameof(WorkPlaceDto.Bookings), x =>
                    x.MapFrom(src => src.Bookings
                    .Select(booking => new BookingDto
                    {
                        Date = booking.Date,
                        DayNumber = booking.DayNumber,
                        Id = booking.Id,
                        Status = booking.Status,
                        Type = booking.Type,
                    }))));

            IMapper mapper = config.CreateMapper();
            return mapper.Map<WorkPlace, WorkPlaceDto>(source);
        }

        public static WorkPlace MapFromDto(WorkPlaceDto source)
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<WorkPlaceDto, WorkPlace>()
                .ForMember(nameof(WorkPlace.Bookings), x =>
                    x.MapFrom(src => src.Bookings
                    .Select(booking => new Booking
                    {
                        Date = booking.Date,
                        DayNumber = booking.DayNumber,
                        Id = booking.Id,
                        Status = booking.Status,
                        Type = booking.Type,
                    }))));

            IMapper mapper = config.CreateMapper();
            return mapper.Map<WorkPlaceDto, WorkPlace>(source);
        }
    }
}
