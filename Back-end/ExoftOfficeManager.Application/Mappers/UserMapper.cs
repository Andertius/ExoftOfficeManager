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
                cfg.CreateMap<User, UserDto>());

            IMapper mapper = config.CreateMapper();
            return mapper.Map<User, UserDto>(source);
        }

        public static User MapFromDto(UserDto source)
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<UserDto, User>());

            IMapper mapper = config.CreateMapper();
            return mapper.Map<UserDto, User>(source);
        }
    }
}
