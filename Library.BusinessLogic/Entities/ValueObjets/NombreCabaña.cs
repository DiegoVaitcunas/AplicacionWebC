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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Library.BusinessLogic.Entities.ValueObjets
{
    [Owned]
    public class NombreCabaña : IValidable
    {
        public string Valor { get; set; }

        #region utilities

        public NombreCabaña(string valor)
        {
            Valor = valor;
        }

        public NombreCabaña() { }

        public void Validation(IConfiguracionRepository config)
        {
            if (Valor is null) throw new IncorrectDataException("El nombre no puede ser nulo");

            if (Valor.Length < config.GetInferiorByName("nombre")) throw new InvalidFormatException("El nombre de la cabaña debe ser mayor");

            if (!Valor.All(c => char.IsLetter(c))) throw new InvalidFormatException("El nombre solo puede contener caracteres alfanumericos");

            if (Valor.Trim() != Valor) throw new InvalidFormatException("El nombre no debe contener espacios");
        }

        #endregion
    }
}
