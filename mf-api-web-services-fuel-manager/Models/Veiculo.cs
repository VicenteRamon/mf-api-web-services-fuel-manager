﻿using System.ComponentModel.DataAnnotations;

namespace mf_api_web_services_fuel_manager.Models
{
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Marca { get; set; }
        [Required]
        public string Modelo { get; set; }
        [Required]
        public string Placa { get; set; }
        [Required]
        public int AnoFabricao { get; set; }
        [Required]
        public int AnoModelo { get; set; }
    }
}