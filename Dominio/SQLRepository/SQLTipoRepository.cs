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
    public class SQLTipoRepository : ITipoRepository
    {
        public ObligatorioContext context { get; set; }
        public SQLTipoRepository()
        {
            context = new ObligatorioContext();
        }
        public void Add(Tipo obj)
        {
            try
            {
                if (context.tipos.ToList().Any(t1 => t1.nombre == obj.nombre))
                {
                    throw new InvalidOperationException("Ya existe un tipo de cabaña con ese nombre.");
                }
                obj.Validation(new SQLConfiguracionRepository());
                context.tipos.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Count()
        {
            return context.tipos.Count();
        }

        public IEnumerable<Tipo> GetAll()
        {
            if (context.tipos.Count() == 0) throw new IsNotFoundException("No se encontraron tipos disponibles");
            return context.tipos;
        }

        public Tipo GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void delete(Tipo obj)
        {
            context.tipos.Remove(obj);
            context.SaveChanges();

        }
        public Tipo getByString(string nombre)
        {
            Tipo ret = context.tipos.Where(t => nombre == t.nombre).FirstOrDefault();
            if (ret != null) return ret;
            throw new ObjetNotFoundException("No se encontro un tipo con ese nombre");
        }

        public void ModificarTipo(string nombre, string nuevaDescripcion, decimal nuevoCostoPorPersona)
        {
            Tipo tipo = this.getByString(nombre);

            if (tipo is null)
            {
                throw new ObjetNotFoundException("No se encontró un tipo con ese nombre");
            }

            if (nuevaDescripcion is null)
            {
                throw new IncorrectDataException("La descripción no puede ser vacia");
            }

            if (nuevoCostoPorPersona <= 0)
            {
                throw new IndexOutOfRangeException("El costo por persona no puede ser menor a cero");
            }

            tipo.descripcion = nuevaDescripcion;
            tipo.costoXHuesped = nuevoCostoPorPersona;

            context.Entry(tipo).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void verificarUso(string t)
        {
            if (context.cabañas.Any(c => c.nombreTipo == t))
            {
                throw new InUseException("El tipo que desea eliminar esta en uso");
            }
            this.delete(this.getByString(t));
        }
    }
}
