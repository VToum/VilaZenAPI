﻿using System.ComponentModel.DataAnnotations;

namespace VilaZen_Web.Models.Dto
{
    public class VillaNumberUpdateDto
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string DetalhesEspeciais { get; set; }

    }
}
