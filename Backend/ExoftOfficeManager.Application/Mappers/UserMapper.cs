using System.Linq;

using AutoMapper;

using ExoftOfficeManager.Domain.Dtos;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.Mappers
{
    public class UserMapper
    {
        public static UserDto MapIntoDto(User source)
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<User, UserDto>()
                .ForMember(nameof(UserDto.Bookings),
                    src => src.MapFrom(x => x.Bookings
                        .Select(booking => new BookingDto
                        {
                            Date = booking.Date,
                            DayNumber = booking.DayNumber,
                            Id = booking.Id,
                            Status = booking.Status,
                            Type = booking.Type,
                        }))));

            IMapper mapper = config.CreateMapper();
            return mapper.Map<User, UserDto>(source);
        }

        public static User MapFromDto(UserDto source)
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<UserDto, User>()
                .ForMember(nameof(User.Bookings),
                    src => src.MapFrom(x => x.Bookings
                        .Select(booking => new Booking
                        {
                            Date = booking.Date,
                            DayNumber = booking.DayNumber,
                            Id = booking.Id,
                            Status = booking.Status,
                            Type = booking.Type,
                        }))));

            IMapper mapper = config.CreateMapper();
            return mapper.Map<UserDto, User>(source);
        }
    }
}
