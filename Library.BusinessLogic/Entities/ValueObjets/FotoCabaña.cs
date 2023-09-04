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
    public class FotoCabaña : IValidable
    {
        public string Valor { get; set; }

        #region utilities

        public FotoCabaña(string valor)
        {
            Valor = valor;
        }

        public FotoCabaña()
        {
        }

        public void Validation(IConfiguracionRepository config)
        {
            if (Valor is null) throw new IncorrectDataException("La foto no puede ser nula");

            if (Valor.IndexOf(".") == -1 || Valor.Length < config.GetInferiorByName("Foto")) throw new InvalidFormatException("El formato de la foto en incorrecto.");

        }

        #endregion 
    }
}
