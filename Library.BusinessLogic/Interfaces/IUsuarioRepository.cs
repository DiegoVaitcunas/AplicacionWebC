using Library.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogic.Interfaces
{
    public interface IUsuarioRepository : IClassRepository<Usuario>
    {
        public Usuario GetByEmail(string email);
        public void Login(string email, string contrasena);
    }
}
