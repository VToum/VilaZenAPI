using System.ComponentModel.DataAnnotations;

namespace VilaZen_Web.Models.Dto
{
    public class VillaNumberCreateDto
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string DetalhesEspeciais { get; set; }
    }
}
