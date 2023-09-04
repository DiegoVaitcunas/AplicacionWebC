using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogic.Interfaces
{
    public interface IClassRepository<T>
    {
        public void Add(T obj);
        public int Count();
        public IEnumerable<T> GetAll();
        public T GetById(int id);
        public void delete(T obj);
    }
}
