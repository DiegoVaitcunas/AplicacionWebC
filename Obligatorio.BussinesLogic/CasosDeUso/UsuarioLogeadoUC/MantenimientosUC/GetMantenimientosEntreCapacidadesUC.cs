using DTOs;
using Library.BusinessLogic.Entities;
using Library.BusinessLogic.Interfaces;
using Obligatorio.LogicaDeAplicacion.InterfacesCasosDeUso.IUsuarioLogeadoUC.IMantenimientosUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio.LogicaDeAplicacion.CasosDeUso.UsuarioLogeadoUC.MantenimientosUC
{
    public class GetMantenimientosEntreCapacidadesUC : IGetMantenimientosEntreCapacidadesUC
    {
        private IMantenimientoRepository mantenimientoRepository;
        public GetMantenimientosEntreCapacidadesUC(IMantenimientoRepository _mantenimientoRepository)
        {
            this.mantenimientoRepository = _mantenimientoRepository;
        }
        public IEnumerable<MantenimientoDTO> GetMantenimientosDeCabañasEntreCapacidades(int cap1, int cap2)
        {
            try
            {

                return mantenimientoRepository.GetMantenimientosDeCabañasEntreCapacidades(cap1, cap2).Select(m => new MantenimientoDTO(m));
            }catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
