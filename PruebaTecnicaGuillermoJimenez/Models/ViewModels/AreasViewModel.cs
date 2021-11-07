using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PruebaTecnicaGuillermoJimenez.Models.ViewModels
{
    public class AreasViewModel
    {
        [Required]
        [Display(Name = "Nombre")]
        [StringLength(100, ErrorMessage = "El {0} deve de tener al menos {1} caracteres", MinimumLength = 1)]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Descripcion")]
        [StringLength(2000, ErrorMessage = "El {0} deve de tener al menos {1} caracteres", MinimumLength = 1)]
        public string Descripcion { get; set; }
    }
    public class EditAreasViewModel
    {
        [Required]
        public int IdArea { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        [StringLength(100, ErrorMessage = "El {0} deve de tener al menos {1} caracteres", MinimumLength = 1)]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Descripcion")]
        [StringLength(2000, ErrorMessage = "El {0} deve de tener al menos {1} caracteres", MinimumLength = 1)]
        public string Descripcion { get; set; }
    }
}