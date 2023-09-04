using DTOs;
using Microsoft.IdentityModel.Tokens;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ISistemaUC.IUsuarioUCSistema;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.IUsuarioUC;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebApi
{
    public class ManejadorJWToken
    {
        private static IObtenerUsuario _GetUser { get; set; }
        public ManejadorJWToken(IObtenerUsuario getUser)
        {
            _GetUser = getUser;
        }
        public static UsuarioDTO getUser(UsuarioDTO usuarioDTO)
        {
            try
            {
                UsuarioDTO usuario = _GetUser.GetByEmail(usuarioDTO.Email);
                return usuario;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public static string GetToken(UsuarioDTO user, IConfiguration config)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email)
            };
            var claveSecreta = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(config.GetSection("AppSettings:SecretTokenKey").Value!));

            var credenciales = new SigningCredentials(claveSecreta, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: credenciales);

            var JwToken = new JwtSecurityTokenHandler().WriteToken(token);

            return JwToken;
        }
    }
}
