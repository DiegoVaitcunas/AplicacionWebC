using DTOs;
using Library.BusinessLogic.Entities;
using Library.BusinessLogic.Interfaces;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ICabañasUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio.BussinesLogic.CasosDeUso.CabañasUC
{
    public class ObtenerCabañasPorCapacidadUC : IObtenerCabañasPorCapacidadUC
    {
        private ICabanaRepository _CabanaRepository;
        public ObtenerCabañasPorCapacidadUC(ICabanaRepository cabanaRepository)
        {
            _CabanaRepository = cabanaRepository;
        }

        public IEnumerable<CabañaDTO> getByCantidad(int cant)
        {
            try
            {
                return _CabanaRepository.getByCantidad(cant).Select(c => new CabañaDTO(c));

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
