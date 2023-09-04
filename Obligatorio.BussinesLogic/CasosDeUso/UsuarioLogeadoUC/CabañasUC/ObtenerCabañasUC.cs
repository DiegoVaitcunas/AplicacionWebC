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
    public class ObtenerCabañasUC : IObtenerCabañasUC
    {
        private ICabanaRepository _CabanaRepository;
        public ObtenerCabañasUC(ICabanaRepository cabanaRepository)
        {
            _CabanaRepository = cabanaRepository;
        }

        public IEnumerable<CabañaDTO> GetAll()
        {
            try
            {
                return _CabanaRepository.GetAll().Select(x => new CabañaDTO(x));
            }catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
