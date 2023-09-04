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
    public class ObtenerCabañasPorTipoUC : IObtenerCabañasPorTipoUC
    {
        private ICabanaRepository _CabanaRepository;
        public ObtenerCabañasPorTipoUC(ICabanaRepository cabanaRepository)
        {
            _CabanaRepository = cabanaRepository;
        }

        public IEnumerable<CabañaDTO> obtenerCabanaPorTipo(Tipo tipo)
        {
            try
            {
                return _CabanaRepository.obtenerCabanaPorTipo(tipo).Select(x => new CabañaDTO(x));
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
