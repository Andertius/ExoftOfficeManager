using AutoMapper;

using ExoftOfficeManager.Domain.Dtos;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.Mappers
{
    public static class BookingMapper
    {
        public static BookingDto MapIntoDto(Booking source)
        {
            var config = new MapperConfiguration(cfg => 
                cfg.CreateMap<Booking, BookingDto>());

            IMapper mapper = config.CreateMapper();
            return mapper.Map<Booking, BookingDto>(source);
        }

        public static Booking MapFromDto(BookingDto source)
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<BookingDto, Booking>());

            IMapper mapper = config.CreateMapper();
            return mapper.Map<BookingDto, Booking>(source);
        }
    }
}
