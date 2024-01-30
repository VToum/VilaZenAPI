using System.ComponentModel.DataAnnotations;

namespace VilaZen_VilaAPI.Models.Dto
{
    public class VillaDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public int Population { get; set; }
        public int Sqft { get; set; }


    }
}
