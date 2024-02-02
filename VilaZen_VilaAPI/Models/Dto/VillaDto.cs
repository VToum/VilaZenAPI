using System.ComponentModel.DataAnnotations;

namespace VilaZen_VilaAPI.Models.Dto
{
    public class VillaDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nome { get; set; }
        public string Detalhes { get; set; }
        [Required]
        public string Avaliar { get; set; }
        public int Ocupacao { get; set; }
        public string ImageUrl { get; set; }
        public string Cortesia { get; set; }

    }
}
