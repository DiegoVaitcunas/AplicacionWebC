using DTOs;
using Library.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio.LogicaDeAplicacion.InterfacesCasosDeUso.IUsuarioLogeadoUC.ICabañasUC
{
    public interface IObtenerCabañaPorMontoyTipoUC
    {
        public IEnumerable<CabañaDTO> GetByMontoAndTipo(string nameTipo, int monto);
    }
}
