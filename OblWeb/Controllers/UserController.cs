using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using ObligatorioP3WebApplication.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ObligatorioP3WebApplication.Controllers
{
    public class UserController : Controller
    {
        static string localhost = "https://localhost:7241";

        #region Login

        public IActionResult Login(string mensaje)
        {
            if (HttpContext.Session.GetString("token") == null){
                ViewBag.mensaje = mensaje;
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Login(UsuarioModel user)
        {
            try
            {
                HttpClient cliente = new HttpClient();

                /******************* HEADERS ******************/

                Uri uri = new Uri($"{localhost}/api/Logins");
                HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Post, uri);

                /******************* CONTENIDO O BODY *******************/

                string json = JsonConvert.SerializeObject(user);
                HttpContent contenido =
                new StringContent(json, Encoding.UTF8, "application/json");
                solicitud.Content = contenido;

                /*************** END CONTENIDO O BODY *******************/

                Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
                respuesta.Wait();

                //Console.WriteLine(respuesta.Result.StatusCode.ToString());

                //Console.WriteLine(respuesta.Result.StatusCode.Equals(System.Net.HttpStatusCode.Unauthorized));

                Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

                UsuarioModel usuarioModel = JsonConvert.DeserializeObject<UsuarioModel>(response.Result);

                HttpContext.Session.SetString("token", usuarioModel.token);

                return RedirectToAction("Index", "Home");
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
            if (HttpContext.Session.GetString("token") != null)
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
