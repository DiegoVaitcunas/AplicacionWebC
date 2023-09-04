using DTOs;
using Library.BusinessLogic.Entities;
using Library.BusinessLogic.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.IMantenimientosUC;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ISistemaUC.ICabañaUCSistema;
using Obligatorio.LogicaDeAplicacion.InterfacesCasosDeUso.IUsuarioLogeadoUC.IMantenimientosUC;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MantenimientosController : Controller
    {
        #region utilities
        private IObtenerMantenimientosEntreFechasUC _ObtenerEntreFechas;
        private IAgregarMantenimientoUC _agregar;
        private IGetMantenimientosEntreCapacidadesUC _getEntreCapacidades;
        public MantenimientosController(IObtenerMantenimientosEntreFechasUC ObtenerEntreFechas, IAgregarMantenimientoUC agregar, IObtenerCabañaPorId getCabaña, IGetMantenimientosEntreCapacidadesUC getEntreCapacidades)
        {
            _ObtenerEntreFechas = ObtenerEntreFechas;
            _agregar = agregar;
            _getEntreCapacidades = getEntreCapacidades;
        }
		#endregion

		#region obtener
		///<Summary>
		///obtener mantenimientos de una cabaña entre dos fechas
		/// </Summary>
		/// <param name="f1">Fecha inicial</param>
		/// <param name="f2">Fecha final</param>
		/// <param name="id">id de la cabaña a buscar</param>
		/// <returns>Listado de mantenimientos para cabañas con ese id y entre esas fechas</returns>
		[HttpGet]
        [Route("/ObtenerEntreFechas/{id}/{f1}/{f2}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult obtenerMantenimientoEntreFechas(int id, DateTime f1, DateTime f2)
        {
            try
            {
                return Ok(_ObtenerEntreFechas.ListarMantenimientosDeCabana(id, f1, f2));
            }
            catch (IsNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }catch (InvalidDataException ex)
            {
                return BadRequest(ex.Message);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

		///<Summary>
		///obtener costo total de los mantenimientos hechos por un trabajador
		/// </Summary>
		/// <param name="cap1">Capacidad menor</param>
		/// <param name="cap2">Capacidad mayor</param>
		/// <returns>Listado de mantenimientos hechos por un trabajador</returns>
		[HttpGet]
        [Route("/ObtenerEntreCapacidades/{cap1}/{cap2}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ObtenerMantenimientoEntreCapacidades(int cap1, int cap2)
        {
            try
            {
                return Ok(_getEntreCapacidades.GetMantenimientosDeCabañasEntreCapacidades(cap1, cap2));
            }
            catch (IsNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }catch (Exception ex)
            {
				return BadRequest(ex.Message);
			}
        }
		#endregion

		#region Alta
		///<Summary>
		///Alta del mantenimiento
		/// </Summary>
		/// <param name="m">Datos del mantenimiento a crear</param>
		/// <returns></returns>
		[HttpPost]
        [Route("/AltaMantenimiento")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize]
        public IActionResult CrearMantenimiento([FromBody] MantenimientoDTO m)
        {
            try
            {
                _agregar.Add(m);
                return Ok();
            }
            catch (limitExceededException ex)
            {
                return BadRequest(ex.Message);
            }catch (InvalidDataException ex)
            {
                return BadRequest(ex.Message);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
