using DTOs;
using Library.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio.LogicaDeAplicacion.InterfacesCasosDeUso.IUsuarioLogeadoUC.IMantenimientosUC
{
    public interface IGetMantenimientosEntreCapacidadesUC
    {
        public IEnumerable<MantenimientoDTO> GetMantenimientosDeCabañasEntreCapacidades(int cap1, int cap2);
    }
}
