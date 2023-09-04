using DTOs;
using Library.BusinessLogic.Entities;
using Library.BusinessLogic.Interfaces;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ISistemaUC.IUsuarioUCSistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio.BussinesLogic.CasosDeUso.SistemaUC.UsuarioUCSistema
{
    public class ObtenerUsuarioUC : IObtenerUsuario
    {
        private IUsuarioRepository _usuarioRepository;
        public ObtenerUsuarioUC(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
            public UsuarioDTO GetByEmail(string email)
        {
                try
                {
                    Library.BusinessLogic.Entities.Usuario aux = _usuarioRepository.GetByEmail(email);
                    UsuarioDTO ret = new UsuarioDTO(aux);
                    return ret;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
    }
}
