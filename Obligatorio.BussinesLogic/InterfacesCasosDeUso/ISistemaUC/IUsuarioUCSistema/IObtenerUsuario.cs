using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio.BussinesLogic.InterfacesCasosDeUso.ISistemaUC.IUsuarioUCSistema
{
    public interface IObtenerUsuario
    {
        public UsuarioDTO GetByEmail(string email);
    }
}
