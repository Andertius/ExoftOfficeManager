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
                cfg.CreateMap<Booking, BookingDto>()
                .ForMember(nameof(BookingDto.User),
                    src => src.MapFrom(x => new UserDto
                    {
                        AvatarUrl = x.User.Avatar,
                        FullName = x.User.FullName,
                        Id = x.User.Id,
                        Role = x.User.Role,
                    }))
                
                .ForMember(nameof(BookingDto.WorkPlace), 
                    src => src.MapFrom(x => new WorkPlaceDto
                    {
                        Id = x.WorkPlace.Id,
                        FloorNumber = x.WorkPlace.FloorNumber,
                        PlaceNumber = x.WorkPlace.PlaceNumber,
                    })));

            IMapper mapper = config.CreateMapper();
            return mapper.Map<Booking, BookingDto>(source);
        }

        public static Booking MapFromDto(BookingDto source)
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<BookingDto, Booking>()
                .ForMember(nameof(Booking.User),
                    src => src.MapFrom(x => new User
                    {
                        Avatar = x.User.AvatarUrl,
                        FullName = x.User.FullName,
                        Id = x.User.Id,
                        Role = x.User.Role,
                    }))

                .ForMember(nameof(BookingDto.WorkPlace),
                    src => src.MapFrom(x => new WorkPlace
                    {
                        Id = x.WorkPlace.Id,
                        FloorNumber = x.WorkPlace.FloorNumber,
                        PlaceNumber = x.WorkPlace.PlaceNumber,
                    })));

            IMapper mapper = config.CreateMapper();
            return mapper.Map<BookingDto, Booking>(source);
        }
    }
}
