using Library.BusinessLogic.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library.BusinessLogic.Exceptions;
using Microsoft.EntityFrameworkCore.Query;
using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using WebApplication1.Models;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ITiposUC;

namespace WebApplication1.Controllers
{
    public class TipoController : Controller
    {

        #region utilities

        private IAgregarTipoUC _Agregar;
        private IEliminarTipoUC _Eliminar;
        private IModificarTipoUC _Modificar;
        private IObtenerTiposUC _obtener;
        private IObtenerTipoPorNombreUC _obtenerPorNombre;
        public TipoController(IAgregarTipoUC agregar, IEliminarTipoUC eliminar, IModificarTipoUC modificar, IObtenerTiposUC obtener, IObtenerTipoPorNombreUC obtenerPorNombre)
        {
            _Agregar = agregar;
            _Eliminar = eliminar;
            _Modificar = modificar;
            _obtener = obtener;
            _obtenerPorNombre = obtenerPorNombre;
        }

        private void ValidarSesion()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                throw new AccessViolationException("No tiene acceso a este contenido ERROR 400");
            }
        }
        #endregion

        #region Home
        public ActionResult index()
        {
            try
            {
                ValidarSesion();
                return View();
            }
            catch (AccessViolationException e)
            {
                return RedirectToAction("Login", "User", new { mensaje = e.Message });
            }
        }
        #endregion

        #region Agregar Tipo
        public ActionResult AgregarTipo(string mensajeError, string mensajeSucces)
        {
            try
            {
                ValidarSesion();
                ViewBag.mensajeError = mensajeError;
                ViewBag.mensajeSucces = mensajeSucces;
                return View();

            }
            catch (AccessViolationException e)
            {
                return RedirectToAction("Login", "User", new { mensaje = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarTipo(Tipo tipo)
        {
            try
            {
                ValidarSesion();
                _Agregar.Add(tipo);
            }
            catch (AccessViolationException e)
            {
                return RedirectToAction("Login", "User", new { mensaje = e.Message });
            }
            catch (InvalidOperationException ex)
            {
                return RedirectToAction("AgregarTipo", "Tipo", new { mensajeError = ex.Message });
            }
            catch (Exception)
            {
                return RedirectToAction("AgregarTipo", "Tipo", new { mensajeError = "Ocurrio un error inesperado" });
            }
            return RedirectToAction("AgregarTipo", "Tipo", new { mensajeSucces = "Se Agrego con exito!" });
        }

        #endregion

        #region listar tipo
        public ActionResult ListarTipo()
        {
            try
            {
                ValidarSesion();
                List<Tipo> list = _obtener.GetAll();
                return View(list);
            }
            catch (AccessViolationException e)
            {
                return RedirectToAction("Login", "User", new { mensaje = e.Message });
            }
            catch (IsNotFoundException ex)
            {
                ViewBag.mensaje = ex.Message;
                return View();
            }
            catch (SqlNullValueException)
            {
                ViewBag.mensaje = "Ocurrio un error inesperado de SQL";
                return View();
            }
            catch (Exception)
            {
                ViewBag.mensaje = "Ocurrio un error inesperado";
                return View();
            }
        }

        #endregion

        #region obtener por nombre
        public ActionResult ObtenerPorNombre(string mensaje)
        {
            try
            {
                ViewBag.mensaje = mensaje;
                return View();
            }
            catch (AccessViolationException e)
            {
                return RedirectToAction("Login", "User", new { mensaje = e.Message });
            }
        }

        public ActionResult ObtenerPorNombreListar(string nombre)
        {
            try
            {
                ValidarSesion();
                Tipo ret = _obtenerPorNombre.getByName(nombre);
                return View(ret);
            }
            catch (AccessViolationException e)
            {
                return RedirectToAction("Login", "User", new { mensaje = e.Message });
            }
            catch (ObjetNotFoundException ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
            catch (SqlException)
            {
                ViewBag.error = "Error 500";
                return View();
            }
            catch (Exception)
            {
                ViewBag.error = "Ocurrio un error inesperado";
                return View();
            }
        }

        #endregion

        #region Buscar para eliminar

        [HttpPost]
        public IActionResult BuscarParaEliminar(string nombre)
        {
            if (nombre != null)
            {
                try
                {
                    ValidarSesion();
                    Tipo tipo = _obtenerPorNombre.getByName(nombre);
                    return View(tipo);
                }
                catch (AccessViolationException e)
                {
                    return RedirectToAction("Login", "User", new { mensaje = e.Message });
                }
                catch (ObjetNotFoundException e)
                {
                    ViewBag.error = e.Message;
                    return View();
                }
                catch (SqlException)
                {
                    ViewBag.error = "Error 500";
                    return View();
                }
                catch (Exception)
                {
                    ViewBag.error = "ocurrio un error inesperado";
                    return View();
                }
            }
            return View();
        }

        public IActionResult Delete(string nombre)
        {
            try
            {
                ValidarSesion();
                _Eliminar.EliminarTipo(nombre);
                return RedirectToAction("ListarTipo", "Tipo", new { mensaje = $"{nombre} se elimino con exito!" });
            }
            catch (AccessViolationException e)
            {
                return RedirectToAction("Login", "User", new { mensaje = e.Message });
            }
            catch (InUseException e)
            {
                return RedirectToAction("ObtenerPorNombre", "Tipo", new { mensaje = e.Message });
            }
            catch (SqlException)
            {
                return RedirectToAction("ObtenerPorNombre", "Tipo", "Error 500");
            }
            catch (Exception)
            {
                return RedirectToAction("ObtenerPorNombre", "Tipo", "ocurrio un error inesperado");
            }
        }
        #endregion

        #region editar
        public IActionResult edit(string nombre)
        {
            try
            {
                ViewBag.nombre = nombre;
                return View();
            }
            catch (AccessViolationException e)
            {
                return RedirectToAction("Login", "User", new { mensaje = e.Message });
            }
        }

        [HttpPost]
        public IActionResult ModificarTipo(Tipo tipo)
        {
            try
            {
                ValidarSesion();
                _Modificar.ModificarTipo(tipo.nombre, tipo.descripcion, tipo.costoXHuesped);
                return RedirectToAction("ObtenerPorNombre", "Tipo", new { mensaje = "Se ha modificado con exito!" });
            }
            catch (AccessViolationException e)
            {
                return RedirectToAction("Login", "User", new { mensaje = e.Message });
            }
            catch (ObjetNotFoundException e)
            {
                return RedirectToAction("ObtenerPorNombre", "Tipo", new { mensaje = e.Message });
            }
            catch (IncorrectDataException e)
            {
                return RedirectToAction("ObtenerPorNombre", "Tipo", new { mensaje = e.Message });
            }
            catch (IndexOutOfRangeException e)
            {
                return RedirectToAction("ObtenerPorNombre", "Tipo", new { mensaje = e.Message });
            }
            catch (SqlException)
            {
                return RedirectToAction("ObtenerPorNombre", "Tipo", "Error 500");
            }
            catch (Exception)
            {
                return RedirectToAction("ObtenerPorNombre", "Tipo", new { mensaje = "ha ocurrido un error inesperado" });
            }
        }
        #endregion
    }
}
