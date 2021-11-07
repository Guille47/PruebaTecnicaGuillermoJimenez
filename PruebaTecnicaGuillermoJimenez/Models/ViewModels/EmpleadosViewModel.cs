using PruebaTecnicaGuillermoJimenez.Models.TableViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PruebaTecnicaGuillermoJimenez.Models.ViewModels
{
    public class EmpleadosViewModel
    {
        [Required]
        [Display(Name = "Nombre Completo")]
        [StringLength(100, ErrorMessage = "El {0} deve de tener al menos {1} caracteres", MinimumLength = 1)]
        public string NombreCompleto { get; set; }
        [Required]
        [Display(Name = "Cedula")]
        [StringLength(50, ErrorMessage = "El {0} deve de tener al menos {1} caracteres", MinimumLength = 1)]
        public string Cedula { get; set; }
        [Required]
        [Display(Name = "Correo")]
        [StringLength(100, ErrorMessage = "El {0} deve de tener al menos {1} caracteres", MinimumLength = 1)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Correo { get; set; }
        [Required]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        [Display(Name = "Fecha de ingreso")]
        public DateTime FechaIngreso { get; set; }
        [Display(Name = "Jefe")]
        public int? IdJefe { get; set; }
        [Required]
        [Display(Name = "Area")]
        public int IdArea { get; set; }
        public byte[] Foto { get; set; }
        [Required]
        [Display(Name = "Foto")]
        public HttpPostedFileBase FotoFile { get; set; }
    }

    public class EditEmpleadosViewModel
    {
        [Required]
        public int IdEmpleado { get; set; }
        [Required]
        [Display(Name = "Nombre Completo")]
        [StringLength(100, ErrorMessage = "El {0} deve de tener al menos {1} caracteres", MinimumLength = 1)]
        public string NombreCompleto { get; set; }
        [Required]
        [Display(Name = "Cedula")]
        [StringLength(50, ErrorMessage = "El {0} deve de tener al menos {1} caracteres", MinimumLength = 1)]
        public string Cedula { get; set; }
        [Required]
        [Display(Name = "Correo")]
        [StringLength(100, ErrorMessage = "El {0} deve de tener al menos {1} caracteres", MinimumLength = 1)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Correo { get; set; }
        [Required]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        [Display(Name = "Fecha de ingreso")]
        public DateTime FechaIngreso { get; set; }
        [Display(Name = "Jefe")]
        public int? IdJefe { get; set; }
        [Required]
        [Display(Name = "Area")]
        public int IdArea { get; set; }
        public byte[] Foto { get; set; }
        [Display(Name = "Foto")]
        public HttpPostedFileBase FotoFile { get; set; }

    }
}