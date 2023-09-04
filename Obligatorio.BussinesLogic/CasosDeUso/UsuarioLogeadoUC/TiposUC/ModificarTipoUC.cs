using Library.BusinessLogic.Interfaces;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ITiposUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio.BussinesLogic.CasosDeUso.TiposUC
{
    public class ModificarTipoUC : IModificarTipoUC
    {
        private ITipoRepository _TipoRepository;
        public ModificarTipoUC(ITipoRepository tipoRepository)
        {
            _TipoRepository = tipoRepository;
        }

        public void ModificarTipo(string nombre, string nuevaDescripcion, decimal nuevoCostoPorPersona)
        {
            try
            {
                _TipoRepository.ModificarTipo(nombre, nuevaDescripcion, nuevoCostoPorPersona);
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
