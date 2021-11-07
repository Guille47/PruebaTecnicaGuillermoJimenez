using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PruebaTecnicaGuillermoJimenez.Models.ViewModels
{
    public class HabilidadesEmpleadoViewModel
    {
        [Required]
        public int IdEmpleado { get; set; }
        [Required]
        [Display(Name = "Nombre de la habilidad")]
        [StringLength(100, ErrorMessage = "El {0} deve de tener al menos {1} caracteres", MinimumLength = 1)]
        public string NombreHabilidad { get; set; }
    }
    public class EditHabilidadesEmpleadoViewModel
    {
        [Required]
        public int IdHabilidad { get; set; }
        [Required]
        public int IdEmpleado { get; set; }
        [Required]
        [Display(Name = "Nombre de la habilidad")]
        [StringLength(100, ErrorMessage = "El {0} deve de tener al menos {1} caracteres", MinimumLength = 1)]
        public string NombreHabilidad { get; set; }
    }
}