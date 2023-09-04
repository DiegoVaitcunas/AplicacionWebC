using Library.BusinessLogic.Entities;
using Library.BusinessLogic.Exceptions;
using Library.BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.AccesData.Memory
{
    public class SQLUsuarioRespository : IUsuarioRepository
    {

        public ObligatorioContext context { get; set; }
        public SQLUsuarioRespository()
        {
            context = new ObligatorioContext();
        }

        public void Add(Usuario obj)
        {
            try
            {
                obj.Validation(new SQLConfiguracionRepository());
                if (GetByEmail(obj.Email) != null)
                {
                    context.usuarios.Add(obj);
                    context.SaveChanges();
                }
                else
                {
                    throw new InUseException("Ya existe un usuario con ese email");
                }
            }
            catch (NotImplementedException ex)
            {
                throw ex;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrio un error inesperado");
            }
        }

        public int Count()
        {
            return context.usuarios.Count();
        }

        public void delete(Usuario obj)
        {
            context.usuarios.Remove(obj);
            context.SaveChanges();
        }

        public IEnumerable<Usuario> GetAll()
        {
            return context.usuarios;
        }
        public Usuario GetById(int id)
        {
            return context.usuarios.ToList()[id];
        }
        public Usuario GetByEmail(string email)
        {
            Usuario retorno = context.usuarios.Where(u => u.Email == email).FirstOrDefault();
            if (retorno != null)
            {
                return retorno;
            }
            throw new ObjetNotFoundException("No se encontro un usuario con ese email");
        }

        public void Login(string email, string contrasena)
        {
            var usuario = GetByEmail(email);
            if (contrasena != usuario.Contrasena.Valor)
            {
                throw new IncorrectDataException("Contraseña incorrecta");
            }
        }
    }
}
