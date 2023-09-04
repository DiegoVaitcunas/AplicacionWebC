using Library.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogic.Interfaces
{
    public interface IMantenimientoRepository : IClassRepository<Mantenimiento>
    {
        public IEnumerable<Mantenimiento> ListarMantenimientosDeCabana(int id, DateTime fechaI, DateTime fechaF);
        public IEnumerable<Mantenimiento> GetMantenimientosDeCabañasEntreCapacidades(int cap1, int cap2);
    }
}