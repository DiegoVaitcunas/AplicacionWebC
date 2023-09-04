using Library.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library.BusinessLogic.Entities;
using WebApplication1.Models;
using Microsoft.Data.SqlClient;
using Library.BusinessLogic.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;
using System.Net.NetworkInformation;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ICabañasUC;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ITiposUC;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [Authorize]
    [ApiConventionType(typeof(DefaultApiConventions))]

    public class CabanaController : Controller
    {

        #region utilities
        private IEliminarCabañaUC _EliminarCabañaRepo;
        private IObtenerCabañaPorNombreUC _ObtenerPorNombreRepo;
        private IObtenerCabañasHabilitadasUC _ObtenerHabilitadasRepo;
        private IObtenerCabañasPorCapacidadUC _ObtenerPorCapacidadRepo;
        private IObtenerCabañasPorTipoUC _ObtenerPorTipoRepo;
        private IObtenerCabañasUC _ObtenerTodasRepo;
        private IRegistrarCabañaUC _RegistrarRepo;
        private IWebHostEnvironment _environment;
        private IObtenerTipoPorNombreUC _obtenerTipo;

        public CabanaController(IEliminarCabañaUC EliminarCabañaRepo, 
            IObtenerCabañaPorNombreUC ObtenerPorNombreRepo, 
            IObtenerCabañasHabilitadasUC ObtenerHabilitadasRepo, 
            IObtenerCabañasPorCapacidadUC ObtenerPorCapacidadRepo, 
            IObtenerCabañasPorTipoUC ObtenerPorTipoRepo, 
            IObtenerCabañasUC ObtenerTodasRepo, 
            IRegistrarCabañaUC RegistrarRepo,
            IWebHostEnvironment environment,
            IObtenerTipoPorNombreUC obtenerTipoPorNombre)
        {
            this._EliminarCabañaRepo = EliminarCabañaRepo;
            this._ObtenerPorNombreRepo = ObtenerPorNombreRepo;
            this._ObtenerHabilitadasRepo = ObtenerHabilitadasRepo;
            this._ObtenerPorCapacidadRepo = ObtenerPorCapacidadRepo;
            this._ObtenerPorTipoRepo = ObtenerPorTipoRepo;
            this._ObtenerTodasRepo = ObtenerTodasRepo;
            this._RegistrarRepo = RegistrarRepo;
            _environment = environment;
            _obtenerTipo = obtenerTipoPorNombre;
        }

        private void ValidarSesion()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                throw new AccessViolationException("No tiene acceso a este contenido ERROR 400");
            }
        }
        public ActionResult Index()
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

        #region Registrar cabaña
        public ActionResult Create()
        {
            try
            {
                ValidarSesion();
                return View(this._ObtenerTodasRepo.GetAll());
            }
            catch (AccessViolationException e)
            {
                return RedirectToAction("Login", "User", new { mensaje = e.Message });
            }
            catch (IsNotFoundException e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
            catch (SqlException)
            {
                return RedirectToAction("Error", "Home", new { error = "Error 400 de SQL" });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cabaña c, IFormFile image)
        {
            try
            {
                ValidarSesion();
                SetDataCabana(c, image);
                _RegistrarRepo.Add(c);
            }
            catch (AccessViolationException e)
            {
                return RedirectToAction("Login", "User", new { mensaje = e.Message });
            }
            catch (InvalidFormatException e)
            {
                ViewBag.error = e.Message;
                return View();
            }
            catch (NotImplementedException e)
            {
                ViewBag.error = e.Message;
                return View();
            }
            catch (InvalidOperationException e)
            {
                ViewBag.error = e.Message;
                return View();
            }
            catch (IsNotFoundException e)
            {
                ViewBag.error = e.Message;
                return View();
            }
            catch (IncorrectDataException e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
            catch (SqlException e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
            ViewBag.success = "Se creo la cabaña con exito!";
            return View();
        }

        private void SetDataCabana(Cabaña c, IFormFile image)
        {
            if (image is null || c is null) throw new IncorrectDataException("Datos incorrectos");
            string rutaFisicaWwwRoot = _environment.WebRootPath;

            string Extension = Path.GetExtension(image.FileName);

            if (Extension != ".png" && Extension != ".gif" && Extension != ".jpeg" && Extension != ".jpg")
            {
                throw new IncorrectDataException("El formato de la imagen es incorrecto");
            }

            string nombreImagen = c.nombre.Valor + "Foto_001" + Path.GetExtension(image.FileName);

            string rutaFisicaAvatar = Path.Combine(rutaFisicaWwwRoot, "Imagenes", "Fotos", nombreImagen);

            try
            {
                using (FileStream f = new FileStream(rutaFisicaAvatar, FileMode.Create))
                {
                    image.CopyTo(f);
                }

                c.Fotos = new Library.BusinessLogic.Entities.ValueObjets.FotoCabaña(nombreImagen);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region BuscarCabana

        #region Generic
        public ActionResult BuscarCabanasPor()
        {
            try
            {
                ValidarSesion();
                IEnumerable<Cabaña> cabañas = _ObtenerTodasRepo.GetAll();
                return View(cabañas);
            }
            catch (AccessViolationException e)
            {
                return RedirectToAction("Login", "User", new { mensaje = e.Message });
            }
            catch (IsNotFoundException e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }

        }

        [HttpPost]
        public ActionResult BuscarCabanasPorDetails(string nombre, int cant)
        {
            try
            {
                ValidarSesion();
                if (nombre != null)
                {
                    return RedirectToAction("BuscarCabanasPorNombre", "Cabana", new { nombre = nombre });

                }
                else if (cant > 0)
                {
                    return RedirectToAction("BuscarCabanasPorCantidad", "Cabana", new { cant = cant });
                }
            }
            catch (AccessViolationException e)
            {
                return RedirectToAction("Login", "User", new { mensaje = e.Message });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
            throw new AccessViolationException("No tienes acceso a este contenido ya que has ingresado de una manera no permitida");
        }
        #endregion

        #region Por nombre
        public ActionResult BuscarCabanasPorNombre(string nombre)
        {
            try
            {
                if (nombre != null)
                {
                    return View(_ObtenerPorNombreRepo.getByNombre(nombre));
                }
                else
                {
                    return View();
                }
            }
            catch (IsNotFoundException e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }
        #endregion

        #region Por tipo
        public ActionResult BuscarCabanasPorTipo()
        {
            try
            {
                return View(_ObtenerTodasRepo.GetAll().ToList());
            }
            catch (IsNotFoundException e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult ListarCabanasPorTipo(string nombreT)
        {
            try
            {
                return View(_ObtenerPorTipoRepo.obtenerCabanaPorTipo(_obtenerTipo.getByName(nombreT)));
            }
            catch (IsNotFoundException e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
            catch (Exception)
            {
                ViewBag.Error = "Ocurrio un error inesperado";
                return View();
            }
        }

        #endregion

        #region Por cantidad
        public ActionResult BuscarCabanasPorCantidad(int cant)
        {
            try
            {
                if (cant > 0)
                {
                    return View(_ObtenerPorCapacidadRepo.getByCantidad(cant));
                }
                else
                {
                    return View();
                }
            }
            catch (IsNotFoundException e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }
        #endregion

        #region Por habilitadas
        public ActionResult BuscarCabanasHabilitadas()
        {
            try
            {
                ValidarSesion();
                return View(_ObtenerHabilitadasRepo.GetHabilitadas());
            }
            catch (AccessViolationException e)
            {
                return RedirectToAction("Login", "User", new { mensaje = e.Message });
            }
            catch (IsNotFoundException e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }
        #endregion


        #endregion

    }
}
