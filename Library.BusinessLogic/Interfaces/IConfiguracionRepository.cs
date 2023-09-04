using System;
using System.Collections.Generic;
using Library.BusinessLogic.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogic.Interfaces
{
    public interface IConfiguracionRepository : IClassRepository<Configuracion>
    {
        public int GetInferiorByName(string name);
        public int GetSuperiorByName(string name);
        public DateTime GetSuperiorByNameDate(string name);
        public DateTime GetInferiorByNameDate(string name);
    }
}
