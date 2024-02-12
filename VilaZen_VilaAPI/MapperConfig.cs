using AutoMapper;
using VilaZen_VilaAPI.Models;
using VilaZen_VilaAPI.Models.Dto;

namespace VilaZen_VilaAPI
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            #region Villa
            CreateMap<Villa, VillaDto>();
            CreateMap<VillaDto, Villa>();

            CreateMap<Villa, VillaCreateDto>().ReverseMap();
            CreateMap<Villa, VillaUpdateDto>().ReverseMap();
            #endregion

            #region VillaNumber
            CreateMap<VillaNumber, VillaNumberDto>();
            CreateMap<VillaNumber, VillaNumberCreateDto>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberUpdateDto>().ReverseMap();
            #endregion

        }
    }
}
