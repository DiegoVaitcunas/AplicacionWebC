using DTOs;
using Library.BusinessLogic.Entities;
using Library.BusinessLogic.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ITiposUC;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TiposController : Controller
    {
        #region utilities
        private IAgregarTipoUC _Agregar;
        private IEliminarTipoUC _Eliminar;
        private IModificarTipoUC _Modificar;
        private IObtenerTiposUC _obtener;
        private IObtenerTipoPorNombreUC _obtenerPorNombre;
        public TiposController(IAgregarTipoUC agregar, IEliminarTipoUC eliminar, IModificarTipoUC modificar, IObtenerTiposUC obtener, IObtenerTipoPorNombreUC obtenerPorNombre)
        {
            _Agregar = agregar;
            _Eliminar = eliminar;
            _Modificar = modificar;
            _obtener = obtener;
            _obtenerPorNombre = obtenerPorNombre;
        }
		#endregion

		#region Alta
		///<Summary>
		///Alta del tipo
		/// </Summary>
		/// <param name="tipo">Datos del tipo a crear</param>
		/// <returns></returns>
		[HttpPost]
        [Route("/AltaTipo")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize]
        public IActionResult CrearTipo([FromBody] TipoDTO tipo)
        {
            try
            {
                _Agregar.Add(tipo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
		#endregion

		#region Eliminar

		///<Summary>
		///Eliminar tipo
		/// </Summary>
		/// <param name="nombre">Nombre del tipo a eliminar</param>
		/// <returns></returns>
		[HttpDelete]
        [Route("/BorrarTipo/{nombre}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [Authorize]
        public IActionResult BorrarTipo(string nombre)
        {
            try
            {
                IEnumerable<TipoDTO> Tipos = _obtener.GetAll();
                if (Tipos.IsNullOrEmpty())
                {
                    return NotFound();
                }
                _Eliminar.EliminarTipo(nombre);
                return Ok(Tipos);
            }
            catch (InUseException ex)
            {
                return BadRequest(ex.Message);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
		#endregion

		#region Modificar

		///<Summary>
		///Modificar Tipo
		/// </Summary>
		/// <param name="t">Datos a cambiar</param>
		/// <returns></returns>
		[HttpPut()]
        [Route("/ModificarTipo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public IActionResult EditarTipo([FromBody] Tipo t)
        {
            try
            {
                IEnumerable<TipoDTO> tipos = _obtener.GetAll();
                if (tipos.IsNullOrEmpty())
                {
                    return NotFound();
                }
                _Modificar.ModificarTipo(t.nombre, t.descripcion, t.costoXHuesped);
                return Ok(tipos);
            }
            catch (ObjetNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }catch (Exception ex)
            {
				return BadRequest(ex);
            }
        }
		#endregion

		#region obtener
		///<Summary>
		///Obtener tipos en la base de datos
		/// </Summary>
		/// <returns>Listado de todos los tipos de cabaña</returns>
		[HttpGet]
        [Route("/GetTipos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            try
            {
                return Ok(_obtener.GetAll());
            }
            catch (Exception ex)
            {
				return BadRequest(ex.Message);
            }
        }

		///<Summary>
		///Obtener Tipos por nombre en la base de datos
		/// </Summary>
		/// <param name="nombre">Nombre del Tipo a buscar</param>
		/// <returns>Listado de tipos de cabañas con ese nombre</returns>
		[HttpGet]
        [Route("/GetTipos/PorNombre/{nombre}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByName(string nombre)
        {
            try
            {
                return Ok(_obtenerPorNombre.getByName(nombre));
            }
            catch (Exception ex)
            {
				return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}