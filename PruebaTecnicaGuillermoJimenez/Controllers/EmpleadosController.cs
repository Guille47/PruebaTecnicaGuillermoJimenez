using PruebaTecnicaGuillermoJimenez.Models;
using PruebaTecnicaGuillermoJimenez.Models.TableViewModels;
using PruebaTecnicaGuillermoJimenez.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PruebaTecnicaGuillermoJimenez.Controllers
{
    public class EmpleadosController : Controller
    {

        // GET: Empleados
        public ActionResult Index(int? id)
        {
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"].ToString();

            List<SelectListItem> lstAreas = LstAreas(id);
            List<EmpleadosTableViewModel> lst = null;
            ViewBag.Areas = lstAreas;

            using (ExamenEntities db = new ExamenEntities())
            {
                lst = (from d in db.Empleadoes
                       join a in db.Areas on d.IdArea equals a.IdArea
                       join e in db.Empleadoes on d.IdJefe equals e.IdEmpleado into EmpleadosAreas
                       from ea in EmpleadosAreas.DefaultIfEmpty()
                       orderby d.IdEmpleado
                       select new EmpleadosTableViewModel
                       {
                           IdEmpleado = d.IdEmpleado,
                           NombreCompleto = d.NombreCompleto,
                           Cedula = d.Cedula,
                           Correo = d.Correo,
                           FechaNacimiento = (DateTime)d.FechaNacimiento,
                           FechaIngreso = d.FechaIngreso,
                           IdJefe = d.IdJefe,
                           NombreJefe = ea.NombreCompleto,
                           IdArea = d.IdArea,
                           NombreArea = a.Nombre,
                           Foto = d.Foto
                       }).Where(d => id == null || d.IdArea == id).ToList();

            }
            return View(lst);
        }
        public ActionResult Ver(int? id)
        {
            if (id == null)
            {
                @TempData["Message"] = "Parece que estas tratando de acceder directamente a la url de ver sin dar el parametro de identificacion del registro";
                return Redirect(Url.Content("~/Empleados/"));
            }
            var Hoy = DateTime.Today;
            VerEmpleadosTableViewModel emp = null;
            using (ExamenEntities db = new ExamenEntities())
            {
                emp = (from d in db.Empleadoes
                       join a in db.Areas on d.IdArea equals a.IdArea
                       join e in db.Empleadoes on d.IdJefe equals e.IdEmpleado into EmpleadosAreas
                       from ea in EmpleadosAreas.DefaultIfEmpty()
                       where d.IdEmpleado == id
                       orderby d.IdEmpleado
                       select new VerEmpleadosTableViewModel
                       {
                           IdEmpleado = d.IdEmpleado,
                           NombreCompleto = d.NombreCompleto,
                           Cedula = d.Cedula,
                           Correo = d.Correo,
                           FechaNacimiento = (DateTime)d.FechaNacimiento,
                           FechaIngreso = d.FechaIngreso,
                           IdJefe = d.IdJefe,
                           NombreJefe = ea.NombreCompleto,
                           IdArea = d.IdArea,
                           NombreArea = a.Nombre,
                           Foto = d.Foto,
                           Edad = Hoy.Year - d.FechaNacimiento.Value.Year,
                           AniosLaborando = Hoy.Year - d.FechaIngreso.Year
                       }).FirstOrDefault();
            }
            return View(emp);
        }

        [HttpGet]
        public ActionResult Agregar()
        {
            List<SelectListItem> lstJefes = LstJefes(null);
            List<SelectListItem> lstAreas = LstAreas(null);
            ViewBag.Areas = lstAreas;
            ViewBag.Jefes = lstJefes;
            return View();
        }
        [HttpPost]
        public ActionResult Agregar(EmpleadosViewModel model)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> lstJefes = LstJefes(null);
                List<SelectListItem> lstAreas = LstAreas(null);
                ViewBag.Areas = lstAreas;
                ViewBag.Jefes = lstJefes;
                return View(model);
            }
            try
            {
                using (var db = new ExamenEntities())
                {
                    Empleado oEmpleados = new Empleado();
                    oEmpleados.NombreCompleto = model.NombreCompleto;
                    oEmpleados.Cedula = model.Cedula;
                    oEmpleados.Correo = model.Correo;
                    oEmpleados.FechaNacimiento = model.FechaNacimiento;
                    oEmpleados.FechaIngreso = model.FechaIngreso;
                    oEmpleados.IdJefe = model.IdJefe;
                    oEmpleados.IdArea = model.IdArea;
                    oEmpleados.Foto = new byte[model.FotoFile.ContentLength];
                    model.FotoFile.InputStream.Read(oEmpleados.Foto, 0, model.FotoFile.ContentLength);

                    db.Empleadoes.Add(oEmpleados);
                    db.SaveChanges();
                }
                return Redirect(Url.Content("~/Empleados/"));
            }
            catch (Exception)
            {
                @TempData["Message"] = "No se pudo agregar el registro";
                return Redirect(Url.Content("~/Empleados/"));
            }
            
        }
        [HttpGet]
        public ActionResult Editar(int? Id)
        {
            if (Id == null)
            {
                @TempData["Message"] = "Parece que estas tratando de acceder directamente a la url de editar sin dar el parametro de identificacion del registro";
                return Redirect(Url.Content("~/Empleados/"));
            }

            List<SelectListItem> lstJefes = LstJefes(Id);
            List<SelectListItem> lstAreas = LstAreas(null);

            EditEmpleadosViewModel empleados = new EditEmpleadosViewModel();

            using (var db = new ExamenEntities())
            {
                var oEmpleados = db.Empleadoes.Find(Id);
                empleados.IdEmpleado = oEmpleados.IdEmpleado;
                empleados.NombreCompleto = oEmpleados.NombreCompleto;
                empleados.Cedula = oEmpleados.Cedula;
                empleados.Correo = oEmpleados.Correo;
                empleados.FechaNacimiento = (DateTime)oEmpleados.FechaNacimiento;
                empleados.FechaIngreso = oEmpleados.FechaIngreso;
                empleados.IdJefe = oEmpleados.IdJefe;
                empleados.IdArea = oEmpleados.IdArea;
                empleados.Foto = oEmpleados.Foto;
            }

            ViewBag.Areas = lstAreas;
            ViewBag.Jefes = lstJefes;
            return View(empleados);
        }

        [HttpPost]
        public ActionResult Editar(EditEmpleadosViewModel model)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> lstJefes = LstJefes(model.IdEmpleado);
                List<SelectListItem> lstAreas = LstAreas(null);

                ViewBag.Areas = lstAreas;
                ViewBag.Jefes = lstJefes;
                return View(model);
            }
            try
            {
                using (var db = new ExamenEntities())
                {
                    Empleado oEmpleados = db.Empleadoes.Find(model.IdEmpleado);

                    oEmpleados.IdEmpleado = model.IdEmpleado;
                    oEmpleados.NombreCompleto = model.NombreCompleto;
                    oEmpleados.Cedula = model.Cedula;
                    oEmpleados.Correo = model.Correo;
                    oEmpleados.FechaNacimiento = model.FechaNacimiento;
                    oEmpleados.FechaIngreso = model.FechaIngreso;
                    oEmpleados.IdJefe = model.IdJefe;
                    oEmpleados.IdArea = model.IdArea;
                    if (model.FotoFile != null)
                    {
                        oEmpleados.Foto = new byte[model.FotoFile.ContentLength];
                        model.FotoFile.InputStream.Read(oEmpleados.Foto, 0, model.FotoFile.ContentLength);
                    }

                    db.Entry(oEmpleados).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return Redirect(Url.Content("~/Empleados/"));

                }
            }
            catch (Exception)
            {
                @TempData["Message"] = "No se pudo editar el registro";
                return Redirect(Url.Content("~/Empleados/"));
            }
            
        }
        [HttpPost]
        public ActionResult Eliminar(int Id)
        {
            try
            {
                using (var db = new ExamenEntities())
                {
                    Empleado oEmpleados = db.Empleadoes.Find(Id);
                    db.Empleadoes.Remove(oEmpleados);
                    db.SaveChanges();
                    return Content("1");
                }
            }
            catch (Exception ex)
            {
                return Content("0");
            }
        }

        public List<SelectListItem> LstJefes(int? id)
        {
            List<SelectListItem> lstJefes = null;
            using (ExamenEntities db = new ExamenEntities())
            {
                if (id == null)
                {
                    lstJefes = (from d in db.Empleadoes
                                orderby d.IdEmpleado
                                select new SelectListItem
                                {
                                    Value = d.IdEmpleado.ToString(),
                                    Text = d.NombreCompleto
                                }).ToList();
                }
                else
                {
                    lstJefes = (from d in db.Empleadoes
                                orderby d.IdEmpleado
                                where d.IdEmpleado != id
                                select new SelectListItem
                                {
                                    Value = d.IdEmpleado.ToString(),
                                    Text = d.NombreCompleto
                                }).ToList();
                }
            }
            return lstJefes;
        }
        public List<SelectListItem> LstAreas(int? id)
        {
            List<SelectListItem> lstAreas = null;
            using (ExamenEntities db = new ExamenEntities())
            {

                if (id == null)
                {
                    lstAreas = (from d in db.Areas
                                orderby d.IdArea
                                select new SelectListItem
                                {
                                    Value = d.IdArea.ToString(),
                                    Text = d.Nombre,

                                }).ToList();
                }
                else
                {
                    lstAreas = (from d in db.Areas
                                orderby d.IdArea
                                select new SelectListItem
                                {
                                    Value = d.IdArea.ToString(),
                                    Text = d.Nombre,
                                    Selected = id == d.IdArea ? true : false
                                }).ToList();
                }
            }
            return lstAreas;
        }
    }
}