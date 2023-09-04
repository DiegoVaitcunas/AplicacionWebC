using Library.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogic.Interfaces
{
    public interface ICabanaRepository : IClassRepository<Cabaña>
    {
        public Cabaña getByNombre(string nombre);
        public IEnumerable<Cabaña> getByCantidad(int cant);
        public IEnumerable<Cabaña> obtenerCabanaPorTipo(Tipo tipo);
        public IEnumerable<Cabaña> GetHabilitadas();
        public IEnumerable<Cabaña> GetByMontoAndTipo(string nameTipo, int monto);
    }
}