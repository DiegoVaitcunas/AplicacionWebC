using Library.BusinessLogic.Interfaces;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ITiposUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio.BussinesLogic.CasosDeUso.TiposUC
{
    public class EliminarTipoUC : IEliminarTipoUC
    {
        private ITipoRepository _TipoRepository;
        public EliminarTipoUC(ITipoRepository tipoRepository)
        {
            _TipoRepository = tipoRepository;
        }
          
        public void EliminarTipo(string t)
        {
            try
            {
                _TipoRepository.verificarUso(t);
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
