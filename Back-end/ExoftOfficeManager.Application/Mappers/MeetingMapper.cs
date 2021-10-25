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

                .ForMember(nameof(MeetingDto.RequiredUsers), x =>
                    x.MapFrom(src =>
                        src.RequiredUserMeetings
                        .Select(x => x.RequiredUser)))

                .ForMember(nameof(MeetingDto.NonRequiredUsers), x =>
                    x.MapFrom(src =>
                        src.NotRequiredUserMeetings
                        .Select(x => x.NotRequiredUser))));

            IMapper mapper = config.CreateMapper();
            return mapper.Map<Meeting, MeetingDto>(source);
        }

        public static Meeting MapFromDto(MeetingDto source)
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<MeetingDto, Meeting>());

            IMapper mapper = config.CreateMapper();
            return mapper.Map<MeetingDto, Meeting>(source);
        }
    }
}
