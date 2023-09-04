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
    public class FechaMantenimiento : IValidable
    {
        public DateTime Valor { get; set; }

        #region utilities
        public FechaMantenimiento(DateTime valor)
        {
            Valor = valor;
        }

        public FechaMantenimiento() { }

        public void Validation(IConfiguracionRepository config)
        {
            if (Valor > config.GetSuperiorByNameDate("FechaMantenimiento")) throw new InvalidDataException("La fecha del mantenimiento no puede ser mayor a la de hoy");
            if (Valor < config.GetInferiorByNameDate("FechaMantenimiento")) throw new InvalidDataException("La fecha del mantenimiento no puede ser inferior a " + config.GetInferiorByNameDate("FechaMantenimiento"));
        }

        #endregion
    }
}
