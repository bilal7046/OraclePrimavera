using AutoMapper;
using OraclePrimavera.Data;
using OraclePrimavera.DTOs;

namespace OraclePrimavera.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProjectRecord, ProjectRecordDTO>().ReverseMap();
        }
    }
}