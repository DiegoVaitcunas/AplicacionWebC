using DTOs;
using Library.BusinessLogic.Entities;
using Library.BusinessLogic.Interfaces;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ICabañasUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Obligatorio.BussinesLogic.CasosDeUso.CabañasUC
{
    public class ObtenerCabañasHabilitadasUC : IObtenerCabañasHabilitadasUC
    {
        private ICabanaRepository _CabanaRepository;
        public ObtenerCabañasHabilitadasUC(ICabanaRepository cabanaRepository)
        {
            _CabanaRepository = cabanaRepository;
        }

        public IEnumerable<CabañaDTO> GetHabilitadas()
        {
            try
            {
                IEnumerable<Cabaña> aux = _CabanaRepository.GetHabilitadas();
                return aux.Select(c => new CabañaDTO(c));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
