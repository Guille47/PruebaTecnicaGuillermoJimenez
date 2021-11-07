using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PruebaTecnicaGuillermoJimenez.Models;
using PruebaTecnicaGuillermoJimenez.Models.TableViewModels;
using PruebaTecnicaGuillermoJimenez.Models.ViewModels;

namespace PruebaTecnicaGuillermoJimenez.Controllers
{
    public class AreasController : Controller
    {
        // GET: Areas
        public ActionResult Index()
        {
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"].ToString();

            List<AreasTableViewModel> lst = null;
            using (ExamenEntities db = new ExamenEntities())
            {
                lst = (from d in db.Areas
                       orderby d.IdArea
                       select new AreasTableViewModel
                       {
                           IdArea = d.IdArea,
                           Nombre = d.Nombre,
                           Descripcion = d.Descripcion
                       }).ToList();
            }
            return View(lst);
        }
        [HttpGet]
        public ActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Agregar(AreasViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                using (var db = new ExamenEntities())
                {
                    Area oAreas = new Area();
                    oAreas.Nombre = model.Nombre;
                    oAreas.Descripcion = model.Descripcion;
                    db.Areas.Add(oAreas);
                    db.SaveChanges();
                }
                return Redirect(Url.Content("~/Areas/"));
            }
            catch (Exception ex)
            {
                @TempData["Message"] = "No se pudo agregar el registro";
                return Redirect(Url.Content("~/Areas/"));
            }
            
        }
        [HttpGet]
        public ActionResult Editar(int? Id)
        {
            if (Id == null)
            {
                @TempData["Message"] = "Parece que estas tratando de acceder directamente a la url de edicion sin dar el parametro de identificacion del registro";
                return Redirect(Url.Content("~/Areas/"));
            }
            EditAreasViewModel model = new EditAreasViewModel();

            using (var db = new ExamenEntities())
            {
                var oAreas = db.Areas.Find(Id);
                model.IdArea = oAreas.IdArea;
                model.Nombre = oAreas.Nombre;
                model.Descripcion = oAreas.Descripcion;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(EditAreasViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                using (var db = new ExamenEntities())
                {
                    Area oAreas = db.Areas.Find(model.IdArea);

                    oAreas.Nombre = model.Nombre;
                    oAreas.Descripcion = model.Descripcion;

                    db.Entry(oAreas).State = System.Data.Entity.EntityState.Modified;

                    db.SaveChanges();

                    return Redirect(Url.Content("~/Areas/"));

                }
            }
            catch (Exception ex)
            {
                @TempData["Message"] = "No se pudo editar el registro";
                return Redirect(Url.Content("~/Areas/"));
            }
        }
        [HttpPost]
        public ActionResult Eliminar(int Id)
        {
            try
            {
                using (var db = new ExamenEntities())
                {
                    Area oAreas = db.Areas.Find(Id);
                    db.Areas.Remove(oAreas);
                    db.SaveChanges();
                    return Content("1");
                }
            }
            catch (Exception ex)
            {
                return Content("0");
            }
        }
    }
}