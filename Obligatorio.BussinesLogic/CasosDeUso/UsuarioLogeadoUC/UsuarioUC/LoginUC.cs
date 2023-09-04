using Library.BusinessLogic.Interfaces;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.IUsuarioUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio.BussinesLogic.CasosDeUso.Usuario
{
    public class LoginUC : ILoginUC
    {
        private IUsuarioRepository _UserRepository;
        public LoginUC(IUsuarioRepository userRepository)
        {
            _UserRepository = userRepository;
        }

        public void Login(string email, string contrasena)
        {
            Console.WriteLine("Estamos aca");
            try
            {
                _UserRepository.Login(email, contrasena);
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
