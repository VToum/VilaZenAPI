﻿using System.ComponentModel.DataAnnotations;

namespace VilaZen_VilaAPI.Models.Dto
{
    public class VillaUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nome { get; set; }
        [Required]
        public string Detalhes { get; set; }
        [Required]
        public double Avaliar { get; set; }
        [Required]
        public int Ocupacao { get; set; }
        [Required]
        public int Sqft { get; set; }
        public string ImageUrl { get; set; }
        public string Cortesia { get; set; }

    }
}