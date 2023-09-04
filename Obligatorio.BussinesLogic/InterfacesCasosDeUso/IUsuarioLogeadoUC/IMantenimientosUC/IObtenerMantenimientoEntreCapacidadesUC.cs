using Library.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio.BussinesLogic.InterfacesCasosDeUso.IMantenimientosUC
{
    public interface IObtenerMantenimientoEntreCapacidadesUC
    {
        public List<Mantenimiento> GetMantenimientosDeCabañasEntreCapacidades(int cap1, int cap2);
    }
}
