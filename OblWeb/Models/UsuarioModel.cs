
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace ObligatorioP3WebApplication.Models
{
    public class UsuarioModel
    {
        public string token { get; set; }
        public string Email { get; set; }
        public PasswordUsuarioModel Contrasena { get; set; }
        public string NombreCompleto { get; set; }
        public UsuarioModel() { }
    }
    public class PasswordUsuarioModel
    {
        public string Valor { get; set; }
    }
}
