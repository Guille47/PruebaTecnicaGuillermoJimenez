using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaTecnicaGuillermoJimenez.Models.TableViewModels
{
    public class HabilidadEmpleadoTableViewModel
    {
        public int IdHabilidad { get; set; }
        public int IdEmpleado { get; set; }
        public string NombreHabilidad { get; set; }
    }
    public class SugerirHabilidadEmpleadoTableViewModel
    {
        public string NombreHabilidad { get; set; }
    }
}