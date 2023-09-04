using DTOs;
using Library.BusinessLogic.Entities;
using Library.BusinessLogic.Exceptions;
using Library.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ICabañasUC;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ISistemaUC.ICabañaUCSistema;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ITiposUC;
using Obligatorio.LogicaDeAplicacion.CasosDeUso.UsuarioLogeadoUC.CabañasUC;
using Obligatorio.LogicaDeAplicacion.InterfacesCasosDeUso.IUsuarioLogeadoUC.ICabañasUC;
using System.Threading;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CabañasController : ControllerBase
    {
        #region utilities
        private readonly ILogger<CabañasController> _logger;
        private IEliminarCabañaUC _EliminarCabañaRepo;
        private IObtenerCabañaPorNombreUC _ObtenerPorNombreRepo;
        private IObtenerCabañasHabilitadasUC _ObtenerHabilitadasRepo;
        private IObtenerCabañasPorCapacidadUC _ObtenerPorCapacidadRepo;
        private IObtenerCabañasPorTipoUC _ObtenerPorTipoRepo;
        private IObtenerCabañasUC _ObtenerTodasRepo;
        private IRegistrarCabañaUC _RegistrarRepo;
        private IWebHostEnvironment _environment;
        private IObtenerTipoPorNombreUC _obtenerTipo;
        private IObtenerCabañaPorMontoyTipoUC _obtenerPorMontoYTipo;
        private IObtenerCabañaPorId _obtenerCabañaPorId;

        public CabañasController(IEliminarCabañaUC EliminarCabañaRepo,
            IObtenerCabañaPorNombreUC ObtenerPorNombreRepo,
            IObtenerCabañasHabilitadasUC ObtenerHabilitadasRepo,
            IObtenerCabañasPorCapacidadUC ObtenerPorCapacidadRepo,
            IObtenerCabañasPorTipoUC ObtenerPorTipoRepo,
            IObtenerCabañasUC ObtenerTodasRepo,
            IRegistrarCabañaUC RegistrarRepo,
            IWebHostEnvironment environment,
            IObtenerTipoPorNombreUC obtenerTipoPorNombre,
            IObtenerCabañaPorMontoyTipoUC obtenerPorMontoYTipo,
            IObtenerCabañaPorId obtenerCabañaPorId)
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
            _obtenerPorMontoYTipo = obtenerPorMontoYTipo;
            this._obtenerCabañaPorId = obtenerCabañaPorId;
        }
		#endregion

		#region Listados

		///<Summary>
		///Obtener cabañas
		/// </Summary>
		/// <returns>Listado de todas las cabañas</returns>
		[HttpGet]
        [Route("/GetCabañas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            try
            {
                return Ok(_ObtenerTodasRepo.GetAll());
            }
            catch (IsNotFoundException ex)
            {
				return StatusCode(404, ex.Message);
			}catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
		}

		///<Summary>
		///Obtener cabañas por tipo
		/// </Summary>
		/// <param name="tipo">Nombre del tipo a buscar</param>
		/// <returns>Listado de cabañas con ese tipo</returns>
		[HttpGet]
        [Route("/GetCabañas/PorTipo/{tipo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPorTipo(string tipo)
        {
            try
            {
                Tipo t = new Tipo();
                TipoDTO aux = _obtenerTipo.getByName(tipo);
                t.nombre = aux.nombre;
                t.descripcion = aux.descripcion;
                t.costoXHuesped = aux.costoXHuesped;
                return Ok(_ObtenerPorTipoRepo.obtenerCabanaPorTipo(t));
            }
			catch (IsNotFoundException ex)
			{
				return StatusCode(404, ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		///<Summary>
		///Obtener cabañas por nombre
		/// </Summary>
		/// <param name="nombre">Nombre de la cabaña a buscar</param>
		/// <returns>Listado de cabañas con ese nombre</returns>
		[HttpGet]
        [Route("/GetCabañas/PorNombre/{nombre}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPorNombte(string nombre)
        {
            try
            {
                return Ok(_ObtenerPorNombreRepo.getByNombre(nombre));
            }
			catch (IsNotFoundException ex)
			{
				return StatusCode(404, ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		///<Summary>
		///Obtener cabañas por capacidad
		/// </Summary>
		/// <param name="capacidad">Capacidad de la cabaña</param>
		/// <returns>Listado de cabañas con esa capacidad o menor</returns>
		[HttpGet]
        [Route("/GetCabañas/PorCapacidad/{capacidad}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPorCapacidad(int capacidad)
        {
            try
            {
                return Ok(_ObtenerPorCapacidadRepo.getByCantidad(capacidad));
            }
			catch (IsNotFoundException ex)
			{
				return StatusCode(404, ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		///<Summary>
		///Obtener cabañas habilitadas
		/// </Summary>
		/// <returns>Listado de cabañas habilitadas</returns>
		[HttpGet]
        [Route("/GetCabañas/Habilitadas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetHabilitadas()
        {
            try
            {
                return Ok(_ObtenerHabilitadasRepo.GetHabilitadas());
            }
			catch (IsNotFoundException ex)
			{
				return StatusCode(404, ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		///<Summary>
		///Obtener cabañas por monto y tipo
		/// </Summary>
		/// <param name="nameTipo">Nombre del tipo</param>
		/// <param name="monto">Monto</param>
		/// <returns>Listado de cabañas con ese tipo por ese monto que esten habilitadas y tengas Jacuzzi</returns>
		[HttpGet]
        [Route("/GetCabañas/PorMontoyTipo/{nameTipo}/{monto}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPorMontoyTipo(string nameTipo, int monto)
        {
            try
            {
                return Ok(_obtenerPorMontoYTipo.GetByMontoAndTipo(nameTipo, monto));
            }
			catch (IsNotFoundException ex)
			{
				return StatusCode(404, ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		///<Summary>
		///Obtener cabañas por id
		/// </Summary>
		/// <param name="id">Id de la cabaña a buscar</param>
		/// <returns>Listado de cabañas con ese id</returns>
		[HttpGet]
        [Route("/GetCabañas/PorId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPorId(int id)
        {
            try
            {
                return Ok(_obtenerCabañaPorId.GetById(id));
            }
			catch (IsNotFoundException ex)
			{
				return StatusCode(404, ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}

		}

		#endregion

		#region alta

		///<Summary>
		///Registrar cabañas
		/// </Summary>
		/// <param name="c">Datos de la cabaña a crear</param>
		/// <returns></returns>
		[HttpPost]
        [Route("/AltaCabaña")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize]
        public IActionResult CrearCabaña([FromBody] CabañaDTO c)
        {
            try
            {
                _RegistrarRepo.Add(c);
                return Ok(_ObtenerPorNombreRepo.getByNombre(c.nombre.Valor));
            }
            catch (InvalidFormatException ex)
            {
                return StatusCode(400, ex.Message);
            }catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

		#endregion

		#region Eliminar
		///<Summary>
		///Eliminar cabañas
		/// </Summary>
		/// <param name="obj">Datos de la cabaña a eliminar</param>
		/// <returns></returns>
		/*[HttpDelete]
        [Route("/EliminarCabaña")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public IActionResult EliminarCabaña([FromBody] CabañaDTO obj)
        {
            try
            {
                IEnumerable<CabañaDTO> cabañas = _ObtenerTodasRepo.GetAll();
                if (cabañas.IsNullOrEmpty())
                {
                    return NotFound();
                }
                _EliminarCabañaRepo.delete(obj);
                return Ok(cabañas);
            }
            catch (Exception ex)
            {
				return StatusCode(404, ex.Message);
			}
        }*/
        #endregion
    }
}