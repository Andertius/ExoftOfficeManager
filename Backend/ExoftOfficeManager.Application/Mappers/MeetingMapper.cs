using System.Linq;

using AutoMapper;

using ExoftOfficeManager.Domain.Dtos;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.Mappers
{
    public class MeetingMapper
    {
        public static MeetingDto MapIntoDto(Meeting source)
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<Meeting, MeetingDto>()

                .ForMember(nameof(MeetingDto.Owner),
                    src => src.MapFrom(x => new UserDto
                    {
                        AvatarUrl = x.Owner.Avatar,
                        FullName = x.Owner.FullName,
                        Id = x.Owner.Id,
                        Role = x.Owner.Role,
                    }))

                .ForMember(nameof(MeetingDto.RequiredUsers), x =>
                    x.MapFrom(src =>
                        src.RequiredUserMeetings.Select(x => x.RequiredUser)))

                .ForMember(nameof(MeetingDto.NonRequiredUsers), x =>
                    x.MapFrom(src =>
                        src.NotRequiredUserMeetings
                            .Select(x => new UserDto
                            {
                                AvatarUrl = x.NotRequiredUser.Avatar,
                                FullName = x.NotRequiredUser.FullName,
                                Id = x.NotRequiredUser.Id,
                                Role = x.NotRequiredUser.Role,
                            }))));

            IMapper mapper = config.CreateMapper();
            return mapper.Map<Meeting, MeetingDto>(source);
        }

        public static Meeting MapFromDto(MeetingDto source)
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<MeetingDto, Meeting>()

                .ForMember(nameof(Meeting.Owner),
                    src => src.MapFrom(x => new User
                    {
                        Avatar = x.Owner.AvatarUrl,
                        FullName = x.Owner.FullName,
                        Id = x.Owner.Id,
                        Role = x.Owner.Role,
                    })));

            IMapper mapper = config.CreateMapper();
            return mapper.Map<MeetingDto, Meeting>(source);
        }
    }
}
