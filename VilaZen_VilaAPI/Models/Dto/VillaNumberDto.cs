﻿using System.ComponentModel.DataAnnotations;

namespace VilaZen_VilaAPI.Models.Dto
{
    public class VillaNumberDto
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaID { get; set; }
        public string DetalhesEspeciais { get; set; }
        public VillaDto Villa { get; set; }

    }
}
