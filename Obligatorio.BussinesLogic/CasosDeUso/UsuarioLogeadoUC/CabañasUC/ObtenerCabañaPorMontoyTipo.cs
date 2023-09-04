using DTOs;
using Library.BusinessLogic.Entities;
using Library.BusinessLogic.Interfaces;
using Obligatorio.LogicaDeAplicacion.InterfacesCasosDeUso.IUsuarioLogeadoUC.ICabañasUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio.LogicaDeAplicacion.CasosDeUso.UsuarioLogeadoUC.CabañasUC
{
    public class ObtenerCabañaPorMontoyTipo : IObtenerCabañaPorMontoyTipoUC
    {
        private ICabanaRepository cabanaRepository;
        public ObtenerCabañaPorMontoyTipo(ICabanaRepository cabanaRepository)
        {
            this.cabanaRepository = cabanaRepository;
        }

        public IEnumerable<CabañaDTO> GetByMontoAndTipo(string nameTipo, int monto)
        {
            try
            {
                return cabanaRepository.GetByMontoAndTipo(nameTipo, monto).Select(x => new CabañaDTO(x));
            }
            catch(Exception ex) {
                throw ex;
            }
        }
    }
}
