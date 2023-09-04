using DTOs;
using Library.BusinessLogic.Entities;
using Library.BusinessLogic.Interfaces;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.IMantenimientosUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio.BussinesLogic.CasosDeUso.MantenimientosUC
{
    public class ObtenerMantenimientoEntreFechasUC : IObtenerMantenimientosEntreFechasUC
    {
        private IMantenimientoRepository _MantenimientoRepository;
        public ObtenerMantenimientoEntreFechasUC(IMantenimientoRepository mantenimientoRepository)
        {
            _MantenimientoRepository = mantenimientoRepository;
        }

        public IEnumerable<MantenimientoDTO> ListarMantenimientosDeCabana(int id, DateTime fechaI, DateTime fechaF)
        {
            try
            {
                return _MantenimientoRepository.ListarMantenimientosDeCabana(id, fechaI, fechaF).Select(m => new MantenimientoDTO(m));
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
