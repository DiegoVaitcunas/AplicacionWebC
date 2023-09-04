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
using System.Globalization;

namespace ObligatorioP3WebApplication.Controllers
{
    public class MantenimientoController : Controller
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

        #region AgregarMantenimiento

        public ActionResult HacerMantenimiento(int id, string error)
        {
            try
            {
                ValidarSesion();
                ViewBag.Error = error;
                return View(getCabañaPorId(id));
            }
            catch (AccessViolationException e)
            {
                return RedirectToAction("Login", "User", new { mensaje = e.Message });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { error = ex.Message });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HacerMantenimiento(MantenimientoModel mantenimiento)
        {
            try
            {
                ValidarSesion();

                HttpClient cliente = new HttpClient();

                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                Uri uri = new Uri($"{localhost}/AltaMantenimiento");
                HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Post, uri);

                /******************* CONTENIDO O BODY ********************/

                string json = JsonConvert.SerializeObject(mantenimiento);
                HttpContent contenido =
                new StringContent(json, Encoding.UTF8, "application/json");
                solicitud.Content = contenido;

                /*************** END CONTENIDO O BODY ********************/

                Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
                respuesta.Wait();

                Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

                MantenimientoModel mant = JsonConvert.DeserializeObject<MantenimientoModel>(response.Result);

                ViewBag.success = "Se agrego el mantenimiento con exito!";
                return View();
            }
            catch (AccessViolationException e)
            {
                return RedirectToAction("Login", "User", new { mensaje = e.Message });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        #endregion

        #region ListarMantenimientoDeCabana

        [HttpGet]
        public IActionResult VerMantenimientos(int id)
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
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
        }

        [HttpPost]
        public IActionResult VerMantenimientos(int id, DateOnly t1, DateOnly t2)
        {
            try
            {
                ValidarSesion();

				//string t1string = t1.ToString().Replace('/', '-');
                //string t2string = t2.ToString().Replace('/', '-');
                
                string t1string = $"{t1.Month}-{t1.Month}-{t1.Year}";
                string t2string = $"{t2.Month}-{t2.Month}-{t2.Year}";

                HttpClient cliente = new HttpClient();
                Uri uri = new Uri($"{localhost}/ObtenerEntreFechas/{id}/{t1string}/{t2string}");
                HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

                Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
                respuesta.Wait();

                Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

                MantenimientoModel[] mant = JsonConvert.DeserializeObject<MantenimientoModel[]>(response.Result);

                return View(mant);
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
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
        }

        public ActionResult VerMantenimientosDeCabañasEntreCap()
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
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
        }

        [HttpPost]
        public ActionResult VerMantenimientosDeCabañasEntreCapListar(int cap1, int cap2)
        {
            try
            {
                ValidarSesion();

                HttpClient cliente = new HttpClient();
                Uri uri = new Uri($"{localhost}/ObtenerEntreCapacidades/{cap1}/{cap2}");
                HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

                Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
                respuesta.Wait();

                Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

                MantenimientoModel[] mant = JsonConvert.DeserializeObject<MantenimientoModel[]>(response.Result);

                return View(mant);
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
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
        }
        #endregion

        #region obetenerCabañas
        private CabañaModel getCabañaPorId(int id)
        {
            HttpClient cliente = new HttpClient();
            Uri uri = new Uri($"{localhost}/GetCabañas/PorId/{id}");
            HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

            Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
            respuesta.Wait();

            Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

            CabañaModel c = JsonConvert.DeserializeObject<CabañaModel>(response.Result);

            return c;
        }
        #endregion

    }
}
