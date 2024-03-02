using AutoMapper;
using VilaZen_Web.Models.Dto;

namespace VilaZen_Web.Models
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            #region VillaDto

            CreateMap<VillaDto, VillaCreateDto>().ReverseMap();
            CreateMap<VillaDto, VillaUpdateDto>().ReverseMap();
            #endregion

            #region VillaNumberDto
            CreateMap<VillaNumberDto, VillaNumberCreateDto>().ReverseMap();
            CreateMap<VillaNumberDto, VillaNumberUpdateDto>().ReverseMap();
            #endregion

        }
    }
}
