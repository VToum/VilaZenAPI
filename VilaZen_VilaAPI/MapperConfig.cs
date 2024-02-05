using AutoMapper;
using VilaZen_VilaAPI.Models;
using VilaZen_VilaAPI.Models.Dto;

namespace VilaZen_VilaAPI
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Villa, VillaDto>();
            CreateMap<VillaDto, Villa>();

            CreateMap<Villa, VillaCreateDto>().ReverseMap();
            CreateMap<Villa, VillaUpdateDto>().ReverseMap();

        }
    }
}
