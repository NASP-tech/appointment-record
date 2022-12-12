using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace medicalAppointments.Models
{
    public class Paciente
    {
        [Key]
        public int idPaciente { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public string EMail { get; set; }
    }
}