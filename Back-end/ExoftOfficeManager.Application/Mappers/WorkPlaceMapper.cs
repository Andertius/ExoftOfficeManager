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
                cfg.CreateMap<WorkPlace, WorkPlaceDto>());

            IMapper mapper = config.CreateMapper();
            return mapper.Map<WorkPlace, WorkPlaceDto>(source);
        }

        public static WorkPlace MapFromDto(WorkPlaceDto source)
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<WorkPlaceDto, WorkPlace>());

            IMapper mapper = config.CreateMapper();
            return mapper.Map<WorkPlaceDto, WorkPlace>(source);
        }
    }
}
