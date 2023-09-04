using Library.BusinessLogic.Entities.ValueObjets;
using Library.BusinessLogic.Exceptions;
using Library.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library.BusinessLogic.Entities
{
    public class Usuario : IValidable
    {
        [Key, EmailAddress]
        public string Email { get; set; }
        [Required, PasswordPropertyText, Column("Contrasena")]
        public PasswordUsuario Contrasena { get; set; }
        public string NombreCompleto { get; set; }

        #region utilities
        public Usuario(string email, string contrasena, string nombre)
        {
            Contrasena = new PasswordUsuario();
            Contrasena.Valor = contrasena;
            //this.Contrasena = contrasena;
            this.Email = email;
            this.NombreCompleto = nombre;
        }

        public Usuario() { }

        public void Validation(IConfiguracionRepository config)
        {
            if (Email is null || Contrasena is null || NombreCompleto is null) throw new NotImplementedException("El usuario o contrasena no pueden ser vacios");

            if (Email.IndexOf("@") == -1 || Email.IndexOf(".") == -1 || Email.Length < config.GetInferiorByName("Email")) throw new InvalidFormatException("El email del Usuario es incorrecto.");

            //if (Contrasena.Length < config.GetInferiorByName("Contrasena") || !Regex.IsMatch(Contrasena, "[a-z]") || !Regex.IsMatch(Contrasena, "[A-Z]") || !Regex.IsMatch(Contrasena, "[0-9]")) throw new InvalidFormatException("El formato de la contraseña es incorrecto");

            Contrasena.Validation(config);
        }
        #endregion
    }
}
