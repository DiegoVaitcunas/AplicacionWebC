using Library.BusinessLogic.Entities.ValueObjets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.BusinessLogic.Entities;

namespace DTOs
{
    public class UsuarioDTO
    {
        public UsuarioDTO(Usuario u)
        {
            this.Email = u.Email;
            this.Contrasena = u.Contrasena;
            this.NombreCompleto = u.NombreCompleto;
        }
        public UsuarioDTO() { }
        public string Email { get; set; }
        public PasswordUsuario Contrasena { get; set; }
        public string NombreCompleto { get; set; }
    }
}
