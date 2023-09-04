using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using System.Text;
using ObligatorioP3WebApplication.Models;
using System.Net.Http.Headers;

namespace ObligatorioP3WebApplication.Controllers
{
    public class TipoController : Controller
    {

        #region utilities

        static string localhost = "https://localhost:7241";


        private void ValidarSesion()
        {
            if (HttpContext.Session.GetString("token") == null)
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
        public ActionResult AgregarTipo(TipoModel tipo)
        {
            try
            {
                ValidarSesion();

                HttpClient cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                Uri uri = new Uri($"{localhost}/AltaTipo");
                HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Post, uri);

                /******************* CONTENIDO O BODY ********************/

                string json = JsonConvert.SerializeObject(tipo);
                HttpContent contenido = new StringContent(json, Encoding.UTF8, "application/json");
                solicitud.Content = contenido;

                /*************** END CONTENIDO O BODY ********************/

                Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
                respuesta.Wait();

                Console.WriteLine(respuesta.Result.StatusCode.ToString());

                Console.WriteLine(respuesta.Result.StatusCode.Equals(System.Net.HttpStatusCode.Unauthorized));
                Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

                TipoModel cabañaModel = JsonConvert.DeserializeObject<TipoModel>(response.Result);
            }
            catch (AccessViolationException e)
            {
                return RedirectToAction("Login", "User", new { mensaje = e.Message });
            }
            catch (Exception e)
            {
                return RedirectToAction("AgregarTipo", "Tipo", new { mensajeError = e });
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

                HttpClient cliente = new HttpClient();
                Uri uri = new Uri($"{localhost}/GetTipos");
                HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

                Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
                respuesta.Wait();

                Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

                TipoModel[] tipos = JsonConvert.DeserializeObject<TipoModel[]>(response.Result);

                return View(tipos);

                //List<Tipo> list = _obtener.GetAll();
                //return View(list);
            }
            catch (AccessViolationException e)
            {
                return RedirectToAction("Login", "User", new { mensaje = e.Message });
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

                HttpClient cliente = new HttpClient();
                Uri uri = new Uri($"{localhost}/GetTipos/PorNombre/{nombre}");
                HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

                Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
                respuesta.Wait();

                Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

                TipoModel tipos = JsonConvert.DeserializeObject<TipoModel>(response.Result);

                return View(tipos);
                //Tipo ret = _obtenerPorNombre.getByName(nombre);
                //return View(ret);
            }
            catch (AccessViolationException e)
            {
                return RedirectToAction("Login", "User", new { mensaje = e.Message });
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

                    HttpClient cliente = new HttpClient();
                    Uri uri = new Uri($"{localhost}/GetTipos/PorNombre/{nombre}");
                    HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

                    Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
                    respuesta.Wait();

                    Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

                    TipoModel tipos = JsonConvert.DeserializeObject<TipoModel>(response.Result);

                    return View(tipos);
                }
                catch (AccessViolationException e)
                {
                    return RedirectToAction("Login", "User", new { mensaje = e.Message });
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
                HttpClient cliente = new HttpClient();
                Uri uri = new Uri($"{localhost}/BorrarTipo/{nombre}");

                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                var tarea = cliente.DeleteAsync(uri);
                tarea.Wait();

                HttpResponseMessage respuesta = tarea.Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToAction("ListarTipo", "Tipo", new { mensaje = $"{nombre} se elimino con exito!" });
                }
                else
                {
                    return RedirectToAction("ObtenerPorNombre", "Tipo", "ocurrio un error inesperado");
                }
            }
            catch (AccessViolationException e)
            {
                return RedirectToAction("Login", "User", new { mensaje = e.Message });
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
        public IActionResult ModificarTipo(TipoModel tipo)
        {
            try
            {
                ValidarSesion();

                HttpClient cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                string url = $"{localhost}/ModificarTipo";

                Task<HttpResponseMessage> tarea = cliente.PutAsJsonAsync(url, tipo);
                tarea.Wait();
                HttpResponseMessage respuesta = tarea.Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToAction("ObtenerPorNombre", "Tipo", new { mensaje = "Se ha modificado con exito!" });
                }
                else
                {
                    return RedirectToAction("ObtenerPorNombre", "Tipo", new { mensaje = "ha ocurrido un error inesperado" });
                }
            }
            catch (AccessViolationException e)
            {
                return RedirectToAction("Login", "User", new { mensaje = e.Message });
            }
            catch (Exception)
            {
                return RedirectToAction("ObtenerPorNombre", "Tipo", new { mensaje = "ha ocurrido un error inesperado" });
            }
        }
        #endregion
    }
}