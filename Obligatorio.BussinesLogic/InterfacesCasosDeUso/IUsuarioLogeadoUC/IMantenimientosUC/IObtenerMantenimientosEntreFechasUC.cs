using DTOs;
using Library.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio.BussinesLogic.InterfacesCasosDeUso.IMantenimientosUC
{
    public interface IObtenerMantenimientosEntreFechasUC
    {
        public IEnumerable<MantenimientoDTO> ListarMantenimientosDeCabana(int id, DateTime fechaI, DateTime fechaF);
    }
}
