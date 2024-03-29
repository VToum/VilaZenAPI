﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VilaZen_VilaAPI.Models
{
    public class Villa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Detalhes { get; set; }
        public double Preco { get; set; }
        public int Ocupacao { get; set; }
        public string ImageUrl { get; set; }
        public string Cortesia { get; set; }
        public int Sqft { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }


    }
}
