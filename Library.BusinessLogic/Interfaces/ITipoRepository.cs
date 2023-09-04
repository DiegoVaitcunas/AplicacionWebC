using Library.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogic.Interfaces
{
    public interface ITipoRepository : IClassRepository<Tipo>
    {
        public Tipo getByString(string name);
        public void verificarUso(string t);
        public void ModificarTipo(string nombre, string nuevaDescripcion, decimal nuevoCostoPorPersona);
    }
}
