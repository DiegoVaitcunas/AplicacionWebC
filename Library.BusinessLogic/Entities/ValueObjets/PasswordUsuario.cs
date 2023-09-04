using Library.BusinessLogic.Exceptions;
using Library.BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library.BusinessLogic.Entities.ValueObjets
{
    [Owned]
    public class PasswordUsuario : IValidable
    {
        public string Valor { get; set; }

        #region utilities

        public PasswordUsuario(string valor)
        {
            Valor = valor;
        }

        public PasswordUsuario() { }

        public void Validation(IConfiguracionRepository config)
        {
            if (Valor is null) throw new IncorrectDataException("La contraseña no puede ser nula");

            if (Valor.Length < config.GetInferiorByName("Contrasena")) throw new InvalidFormatException("El largo de la contraseña no puede ser menor a 6");

            if (!Regex.IsMatch(Valor, "[a-z]") || !Regex.IsMatch(Valor, "[A-Z]") || !Regex.IsMatch(Valor, "[0-9]")) throw new InvalidFormatException("La contraseña debe contener minusculas, mayuscualas y numeros");
        }

        #endregion
    }
}
