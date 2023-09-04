using Library.BusinessLogic.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library.BusinessLogic.Exceptions;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.IMantenimientosUC;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ICabañasUC;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ISistemaUC.ICabañaUCSistema;

namespace WebApplication1.Controllers
{
    public class MantenimientoController : Controller
    {
        #region utilities
        
        private IObtenerMantenimientosEntreFechasUC _ObtenerEntreFechas;
        private IAgregarMantenimientoUC _agregar;
        private IObtenerCabañaPorId _GetCabaña;
        public MantenimientoController(IObtenerMantenimientosEntreFechasUC ObtenerEntreFechas, IAgregarMantenimientoUC agregar, IObtenerCabañaPorId getCabaña) 
        {
            _ObtenerEntreFechas = ObtenerEntreFechas;
            _agregar = agregar;
            _GetCabaña = getCabaña;
        }
        private void ValidarSesion()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                throw new AccessViolationException("No tiene acceso a este contenido ERROR 400");
            }
        }

        #endregion

        #region AgregarMantenimiento

        public ActionResult HacerMantenimiento(int id, string error)
        {
            try
            {
                ValidarSesion();
                ViewBag.Error = error;
                return View(_GetCabaña.GetById(id));
            }
            catch (AccessViolationException e)
            {
                return RedirectToAction("Login", "User", new { mensaje = e.Message });
            }
            catch (ObjetNotFoundException ex)
            {
                return RedirectToAction("Error", "Home", new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { error = ex.Message });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HacerMantenimiento(Mantenimiento mantenimiento)
        {
            try
            {
                _agregar.Add(mantenimiento);
                ViewBag.success = "Se agrego el mantenimiento con exito!";
                return View();
            }
            catch (InvalidDataException e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
            catch (AccessViolationException e)
            {
                return RedirectToAction("Login", "User", new { mensaje = e.Message });
            }
            catch (limitExceededException e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
            catch (IsNotFoundException e)
            {
                return RedirectToAction("HacerMantenimiento", "Mantenimiento", new { error = e.Message });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        #endregion

        #region ListarMantenimientoDeCabana

        public ActionResult VerMantenimientos(int id)
        {
            try
            {
                ValidarSesion();
                ViewBag.id = id;
                return View();
            }
            catch (AccessViolationException e)
            {
                return RedirectToAction("Login", "User", new { mensaje = e.Message });
            }
            catch (IsNotFoundException ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
        }

        [HttpPost]
        public ActionResult VerMantenimientos(int id, DateTime t1, DateTime t2)
        {
            try
            {
                ValidarSesion();
                return View(_ObtenerEntreFechas.ListarMantenimientosDeCabana(id,t1,t2));
            }
            catch (AccessViolationException e)
            {
                return RedirectToAction("Login", "User", new { mensaje = e.Message });
            }
            catch (InvalidDataException e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
            catch (IsNotFoundException ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
        }
        #endregion

    }
}
