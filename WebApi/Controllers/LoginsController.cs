using DTOs;
using Library.BusinessLogic.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ISistemaUC.IUsuarioUCSistema;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.IUsuarioUC;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : Controller
    {
        #region utilities
        private IConfiguration _configuration;
        private ILoginUC _log;
        private ManejadorJWToken ManejadorJWToken;
        public LoginsController(IConfiguration configuration, IObtenerUsuario GetUser, ILoginUC loginUC)
        {
            this._configuration = configuration;
            this.ManejadorJWToken = new ManejadorJWToken(GetUser);
            _log = loginUC;
        }
		#endregion

		///<Summary>
		///Loguearse
		/// </Summary>
		/// <param name="usuarioActual">Datos del usuario a loguearse</param>
		/// <returns></returns>
		[HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<UsuarioDTO> Login([FromBody] UsuarioDTO usuarioActual)
        {
            try
            {
                _log.Login(usuarioActual.Email, usuarioActual.Contrasena.Valor);
                var usuario = ManejadorJWToken.getUser(usuarioActual);
                var token = ManejadorJWToken.GetToken(usuario, _configuration);

                return Ok(new
                {
                    token = token,
                    usuario = usuario
                });
            }
            catch (IncorrectDataException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Entraste a un area restringida");
        }
    }
}
