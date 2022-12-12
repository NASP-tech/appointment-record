using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace medicalAppointments.Models
{
    public class Citas
    {
        [Key]
        public int idCita { get; set; }
        [Required]
        public int idPaciente { get; set; }
        [Required]
        public int idDoctor { get; set; }
        [Required]
        public string dia { get; set; }
        [Required]
        public string hora { get; set; }
        [Required]
        public string diagnostico { get; set; }
        [Required]
        public string comentarios { get; set; }
    }
}