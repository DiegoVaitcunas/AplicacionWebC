using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio.BussinesLogic.InterfacesCasosDeUso.IUsuarioUC
{
    public interface ILoginUC
    {
        public void Login(string email, string contrasena);
    }
}
