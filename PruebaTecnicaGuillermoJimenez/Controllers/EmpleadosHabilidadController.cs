using PruebaTecnicaGuillermoJimenez.Models;
using PruebaTecnicaGuillermoJimenez.Models.TableViewModels;
using PruebaTecnicaGuillermoJimenez.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PruebaTecnicaGuillermoJimenez.Controllers
{
    public class EmpleadosHabilidadController : Controller
    {
        // GET: EmpleadosHabilidad
        public ActionResult Index()
        {
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"].ToString();

            return View();
        }
        public JsonResult EmpleadosJerarquias() {

            List<JerarquiaEmpleadosTableViewModel> lst = null;
            using (ExamenEntities db = new ExamenEntities())
            {
                lst = (from d in db.Empleadoes
                       join a in db.Areas on d.IdArea equals a.IdArea
                       join e in db.Empleadoes on d.IdJefe equals e.IdEmpleado into EmpleadosAreas
                       from ea in EmpleadosAreas.DefaultIfEmpty()
                       orderby d.IdEmpleado
                       select new JerarquiaEmpleadosTableViewModel
                       {
                           IdEmpleado = d.IdEmpleado,
                           NombreCompleto = d.NombreCompleto,
                           IdJefe = d.IdJefe,
                       }).ToList();

                return Json(lst, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult VerHabilidad(int id)
        {
            List<HabilidadEmpleadoTableViewModel> lst = null;
            using (ExamenEntities db = new ExamenEntities())
            {
                lst = (from d in db.Empleado_Habilidad
                       orderby d.NombreHabilidad
                       where d.IdEmpleado == id
                       select new HabilidadEmpleadoTableViewModel
                       {
                           IdHabilidad = d.IdHabilidad,
                           IdEmpleado = d.IdEmpleado,
                           NombreHabilidad = d.NombreHabilidad,
                       }).ToList();

                return Json(lst, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Agregar(int? id)
        {
            if (id == null)
            {
                @TempData["Message"] = "Parece que estas tratando de acceder directamente a la url de ver agregar dar el parametro de identificacion del registro";
                return Redirect(Url.Content("~/EmpleadosHabilidad/"));
            }
            ViewBag.IdEmpleado = id.ToString();
            return View();
        }
        [HttpPost]
        public ActionResult Agregar(HabilidadesEmpleadoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new ExamenEntities())
            {
                Empleado_Habilidad oHabilidadesEmpleados = new Empleado_Habilidad();
                oHabilidadesEmpleados.IdEmpleado = (int)model.IdEmpleado;
                oHabilidadesEmpleados.NombreHabilidad = model.NombreHabilidad;

                db.Empleado_Habilidad.Add(oHabilidadesEmpleados);

                db.SaveChanges();

            }

            return Redirect(Url.Content("~/EmpleadosHabilidad/"));
        }
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                @TempData["Message"] = "Parece que estas tratando de acceder directamente a la url de ver editar dar el parametro de identificacion del registro";
                return Redirect(Url.Content("~/EmpleadosHabilidad/"));
            }
            EditHabilidadesEmpleadoViewModel model = new EditHabilidadesEmpleadoViewModel();

            using (var db = new ExamenEntities())
            {
                var oHabilidadesEmpleados = db.Empleado_Habilidad.Find(id);
                model.IdHabilidad = oHabilidadesEmpleados.IdHabilidad;
                model.IdEmpleado = oHabilidadesEmpleados.IdEmpleado;
                model.NombreHabilidad = oHabilidadesEmpleados.NombreHabilidad;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Editar(EditHabilidadesEmpleadoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new ExamenEntities())
            {
                Empleado_Habilidad oHabilidadesEmpleados = db.Empleado_Habilidad.Find(model.IdHabilidad);

                oHabilidadesEmpleados.IdEmpleado = model.IdEmpleado;
                oHabilidadesEmpleados.NombreHabilidad = model.NombreHabilidad;

                db.Entry(oHabilidadesEmpleados).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();

                return Redirect(Url.Content("~/EmpleadosHabilidad/"));

            }
        }
        public JsonResult Sugerencia()
        {
            List<SugerirHabilidadEmpleadoTableViewModel> lst = null;
            using (ExamenEntities db = new ExamenEntities())
            {
                lst = (from d in db.Empleado_Habilidad
                       orderby d.NombreHabilidad
                       //where d.NombreHabilidad.Contains(palabra)
                       select new SugerirHabilidadEmpleadoTableViewModel
                       {
                           NombreHabilidad = d.NombreHabilidad,
                       }).ToList();

                return Json(lst, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Eliminar(int Id)
        {
            using (var db = new ExamenEntities())
            {
                Empleado_Habilidad oEmpleadosHabilidad = db.Empleado_Habilidad.Find(Id);
                db.Empleado_Habilidad.Remove(oEmpleadosHabilidad);
                db.SaveChanges();
                return Content("1");
            }
        }

    }
}