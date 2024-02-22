using System.ComponentModel.DataAnnotations;

namespace VilaZen_Web.Models.Dto
{
    public class VillaNumberDto
    {
        [Required]
        public int VillaNo { get; set; }
        public string DetalhesEspeciais { get; set; }

    }
}
