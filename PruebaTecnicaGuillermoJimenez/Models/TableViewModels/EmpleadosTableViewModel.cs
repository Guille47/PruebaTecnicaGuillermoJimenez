using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaTecnicaGuillermoJimenez.Models.TableViewModels
{
    public class EmpleadosTableViewModel
    {
        public int IdEmpleado { get; set; }
        public string NombreCompleto { get; set; }
        public string Cedula { get; set; }
        public string Correo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaIngreso { get; set; }
        public int? IdJefe { get; set; }
        public string NombreJefe { get; set; }
        public int IdArea { get; set; }
        public string NombreArea { get; set; }
        public byte[] Foto { get; set; }
    }    
    public class VerEmpleadosTableViewModel
    {
        public int IdEmpleado { get; set; }
        public string NombreCompleto { get; set; }
        public string Cedula { get; set; }
        public string Correo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaIngreso { get; set; }
        public int? IdJefe { get; set; }
        public string NombreJefe { get; set; }
        public int IdArea { get; set; }
        public string NombreArea { get; set; }
        public byte[] Foto { get; set; }

        public double Edad { get; set; }

        public double AniosLaborando { get; set; }
    }

    public class JerarquiaEmpleadosTableViewModel
    {
        public int IdEmpleado { get; set; }
        public string NombreCompleto { get; set; }
        public int? IdJefe { get; set; }

    }

    public class JefesTableViewModel
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }
}