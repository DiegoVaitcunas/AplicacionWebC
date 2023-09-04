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
    public class ObtenerCabañaPorNombreUC : IObtenerCabañaPorNombreUC
    {
        private ICabanaRepository _CabanaRepository;
        public ObtenerCabañaPorNombreUC(ICabanaRepository cabanaRepository)
        {
            _CabanaRepository = cabanaRepository;
        }

        public CabañaDTO getByNombre(string nombre)
        {
            try
            {
                Cabaña aux = _CabanaRepository.getByNombre(nombre);
                CabañaDTO ret = new CabañaDTO(aux);
                return ret;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
