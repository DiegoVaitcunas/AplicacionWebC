using Azure.Core;
using Library.BusinessLogic.Entities;
using Library.BusinessLogic.Exceptions;
using Library.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.IUsuarioUC;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private ILoginUC _log;
        public UserController(ILoginUC log)
        {
            _log = log;
        }


        #region Login
        public IActionResult Login(string mensaje)
        {
            ViewBag.mensaje = mensaje;
            return View();
        }

        [HttpPost]
        public IActionResult login(string Email, string Contrasena)
        {
            try
            {
                _log.Login(Email, Contrasena);
                HttpContext.Session.SetString("Email", Email);
                return RedirectToAction("Index", "Home");
            }
            catch (IncorrectDataException ex)
            {
                return RedirectToAction("Login", "User", new { Mensaje = ex.Message });
            }
            catch (ObjetNotFoundException e)
            {
                return RedirectToAction("Login", "User", new { Mensaje = e.Message });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "User", new { Mensaje = ex.Message });
            }
        }
        #endregion

        #region Logout
        public ActionResult logout()
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "User");
            }
            else
            {
                return RedirectToAction("Error", "Home", new { RequestId = "Usted no tiene una sesion iniciada" });
            }
        }
        #endregion

    }
}
