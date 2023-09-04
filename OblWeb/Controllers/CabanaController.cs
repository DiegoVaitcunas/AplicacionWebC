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
	public class CabanaController : Controller
	{

		#region utilities


		static string localhost = "https://localhost:7241";

		private IWebHostEnvironment _environment { get; set; }
		public CabanaController(
			IWebHostEnvironment environment)
		{
			_environment = environment;
		}

		private void ValidarSesion()
		{
			if (HttpContext.Session.GetString("token") == null)
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
				HttpClient cliente = new HttpClient();

				Uri uri = new Uri($"{localhost}/GetTipos");
				HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

				Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
				respuesta.Wait();

				Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

				TipoModel[] tipos = JsonConvert.DeserializeObject<TipoModel[]>(response.Result);

				return View(tipos);
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
		[ValidateAntiForgeryToken]
		public ActionResult Create(CabañaModel c, IFormFile image)
		{
			try
			{
				ValidarSesion();

				HttpClient cliente = new HttpClient();
				cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

				Uri uri = new Uri($"{localhost}/AltaCabaña");
				HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Post, uri);

				/******************* CONTENIDO O BODY ********************/

				string json = JsonConvert.SerializeObject(c);
				HttpContent contenido = new StringContent(json, Encoding.UTF8, "application/json");
				solicitud.Content = contenido;

				/*************** END CONTENIDO O BODY ********************/

				Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
				respuesta.Wait();

				Console.WriteLine(respuesta.Result.StatusCode.ToString());

				Console.WriteLine(respuesta.Result.StatusCode.Equals(System.Net.HttpStatusCode.Unauthorized));
				Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

				CabañaModel cabañaModel = JsonConvert.DeserializeObject<CabañaModel>(response.Result);

				SetDataCabana(c, image);

				//_RegistrarRepo.Add(c);
				ViewBag.success = "Se creo la cabaña con exito!";
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

		#region Foto
		private void SetDataCabana(CabañaModel c, IFormFile image)
		{
			if (image is null || c is null) throw new Exception("Datos incorrectos");
			string rutaFisicaWwwRoot = _environment.WebRootPath;

			string Extension = Path.GetExtension(image.FileName);

			if (Extension != ".png" && Extension != ".gif" && Extension != ".jpeg" && Extension != ".jpg")
			{
				throw new Exception("El formato de la imagen es incorrecto");
			}

			string nombreImagen = c.nombre.Valor + "Foto_001" + Path.GetExtension(image.FileName);

			string rutaFisicaAvatar = Path.Combine(rutaFisicaWwwRoot, "Imagenes", "Fotos", nombreImagen);

			try
			{
				using (FileStream f = new FileStream(rutaFisicaAvatar, FileMode.Create))
				{
					image.CopyTo(f);
				}

				c.Fotos = new FotoCabañaModel(nombreImagen);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#endregion

		#region BuscarCabana

		#region Generic
		public ActionResult BuscarCabanasPor()
		{
			try
			{
				ValidarSesion();

				HttpClient cliente = new HttpClient();
				Uri uri = new Uri($"{localhost}/GetCabañas");
				HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

				Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
				respuesta.Wait();

				Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

				CabañaModel[] cabañas = JsonConvert.DeserializeObject<CabañaModel[]>(response.Result);

				return View(cabañas);
				//return View(_ObtenerTodasRepo.GetAll(););
			}
			catch (AccessViolationException e)
			{
				return RedirectToAction("Login", "User", new { mensaje = e.Message });
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
					ValidarSesion();

					HttpClient cliente = new HttpClient();
					Uri uri = new Uri($"{localhost}/GetCabañas/PorNombre/{nombre}");
					HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

					Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
					respuesta.Wait();

					Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

					CabañaModel cabañas = JsonConvert.DeserializeObject<CabañaModel>(response.Result);

					return View(cabañas);
					//return View(_ObtenerPorNombreRepo.getByNombre(nombre));
				}
				else
				{
					return View();
				}
			}
			catch (AccessViolationException e)
			{
				return RedirectToAction("Login", "User", new { mensaje = e.Message });
			}
			catch (Exception e)
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
				ValidarSesion();

				HttpClient cliente = new HttpClient();
				Uri uri = new Uri($"{localhost}/GetTipos");
				HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

				Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
				respuesta.Wait();

				Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

				TipoModel[] tipos = JsonConvert.DeserializeObject<TipoModel[]>(response.Result);

				return View(tipos);
			}
			catch (AccessViolationException e)
			{
				return RedirectToAction("Login", "User", new { mensaje = e.Message });
			}
			catch (Exception e)
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
				ValidarSesion();

				HttpClient cliente = new HttpClient();
				Uri uri = new Uri($"{localhost}/GetCabañas/PorTipo{nombreT}");
				HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

				Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
				respuesta.Wait();

				Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

				CabañaModel[] cabañas = JsonConvert.DeserializeObject<CabañaModel[]>(response.Result);

				return View(cabañas);
				//return View(_ObtenerPorTipoRepo.obtenerCabanaPorTipo(_obtenerTipo.getByName(nombreT)));
			}
			catch (AccessViolationException e)
			{
				return RedirectToAction("Login", "User", new { mensaje = e.Message });
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
					ValidarSesion();

					HttpClient cliente = new HttpClient();
					Uri uri = new Uri($"{localhost}/GetCabañas/PorCapacidad/{cant}");
					HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

					Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
					respuesta.Wait();

					Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

					CabañaModel[] cabañas = JsonConvert.DeserializeObject<CabañaModel[]>(response.Result);

					return View(cabañas);
					//return View(_ObtenerPorCapacidadRepo.getByCantidad(cant));
				}
				else
				{
					return View();
				}
			}
			catch (AccessViolationException e)
			{
				return RedirectToAction("Login", "User", new { mensaje = e.Message });
			}
			catch (Exception e)
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

				HttpClient cliente = new HttpClient();
				Uri uri = new Uri($"{localhost}/GetCabañas/Habilitadas");
				HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

				Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
				respuesta.Wait();

				Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

				CabañaModel[] cabañas = JsonConvert.DeserializeObject<CabañaModel[]>(response.Result);

				return View(cabañas);
			}
			catch (AccessViolationException e)
			{
				return RedirectToAction("Login", "User", new { mensaje = e.Message });
			}
			catch (Exception e)
			{
				ViewBag.Error = e.Message;
				return View();
			}
		}
		#endregion

		#region Por Tipo y monto

		public ActionResult BuscarCabanasPorTipoYMonto()
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

			}
			catch (AccessViolationException e)
			{
				return RedirectToAction("Login", "User", new { mensaje = e.Message });
			}
			catch (Exception e)
			{
				ViewBag.Error = e.Message;
				return View();
			}
		}

		public ActionResult BuscarCabanasPorTipoYMontoListar(string nombreT, int monto)
		{
			try
			{
				ValidarSesion();

				HttpClient cliente = new HttpClient();
				Uri uri = new Uri($"{localhost}/GetCabañas/PorMontoyTipo/{nombreT}/{monto}");
				HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, uri);

				Task<HttpResponseMessage> respuesta = cliente.SendAsync(solicitud);
				respuesta.Wait();

				Task<string> response = respuesta.Result.Content.ReadAsStringAsync();

				CabañaModel[] cabañas = JsonConvert.DeserializeObject<CabañaModel[]>(response.Result);

				return View(cabañas);
			}
			catch (AccessViolationException e)
			{
				return RedirectToAction("Login", "User", new { mensaje = e.Message });
			}
			catch (Exception e)
			{
				ViewBag.Error = e.Message;
				return View();
			}
		}

		#endregion

		#endregion

	}
}
