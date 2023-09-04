using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio.BussinesLogic.InterfacesCasosDeUso.ITiposUC
{
    public interface IModificarTipoUC
    {
        public void ModificarTipo(string nombre, string nuevaDescripcion, decimal nuevoCostoPorPersona);
    }
}
