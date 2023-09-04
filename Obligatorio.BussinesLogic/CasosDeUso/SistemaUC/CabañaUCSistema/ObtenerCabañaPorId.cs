using DTOs;
using Library.BusinessLogic.Entities;
using Library.BusinessLogic.Interfaces;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ISistemaUC.ICabañaUCSistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Obligatorio.BussinesLogic.CasosDeUso.SistemaUC.CabañaUCSistema
{
    public class ObtenerCabañaPorId : IObtenerCabañaPorId
    {
        private ICabanaRepository _CabanaRepository;
        public ObtenerCabañaPorId(ICabanaRepository cabanaRepository)
        {
            _CabanaRepository = cabanaRepository;
        }

        public CabañaDTO GetById(int id)
        {
            try
            {
                Cabaña aux = _CabanaRepository.GetById(id);
                CabañaDTO ret = new CabañaDTO(aux);
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
