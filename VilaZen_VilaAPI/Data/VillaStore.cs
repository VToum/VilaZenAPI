using VilaZen_VilaAPI.Models.Dto;

namespace VilaZen_VilaAPI.Data
{
    public static class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto> 
        {
            new VillaDto{Id = 1, Nome = "Vila Rica", Population = 300, Sqft = 10 }
        };
    }
}
