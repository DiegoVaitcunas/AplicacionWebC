
using Library.BusinessLogic.Entities;
using Library.BusinessLogic.Exceptions;
using Library.BusinessLogic.Interfaces;

namespace Library.AccesData.Memory
{
    public class SQLConfiguracionRepository : IConfiguracionRepository
    {
        public ObligatorioContext context { get; set; }
        public SQLConfiguracionRepository()
        {
            context = new ObligatorioContext();
        }
        public void Add(Configuracion item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Configuracion item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Configuracion> GetAll()
        {
            throw new NotImplementedException();
        }

        public Configuracion GetById(int id)
        {
            throw new NotImplementedException();
        }
        public int Count()
        {
            throw new NotImplementedException();
        }

        public void delete(Configuracion obj)
        {
            throw new NotImplementedException();
        }

        public void Update(Configuracion item)
        {
            throw new NotImplementedException();
        }
        public int GetInferiorByName(string name)
        {
            Configuracion config = context.Configuraciones.Where(config => config.Atributo == name).FirstOrDefault();
            if (config == null) throw new IsNotFoundException("No se encontro ese atributo");
            return (int)config.LimiteInferior;
        }

        public int GetSuperiorByName(string name)
        {
            Configuracion config = context.Configuraciones.Where(config => config.Atributo == name).FirstOrDefault();
            if (config == null) throw new IsNotFoundException("No se encontro ese atributo");
            return (int)config.LimiteSuperior;
        }

        public DateTime GetSuperiorByNameDate(string name)
        {
            Configuracion config = context.Configuraciones.Where(config => config.Atributo == name).FirstOrDefault();
            if (config == null) throw new IsNotFoundException("No se encontro ese atributo");
            return (DateTime)config.LimiteSuperiorDate;
        }

        public DateTime GetInferiorByNameDate(string name)
        {
            Configuracion config = context.Configuraciones.Where(config => config.Atributo == name).FirstOrDefault();
            if (config == null) throw new IsNotFoundException("No se encontro ese atributo");
            return (DateTime)config.LimiteInferiorDate;
        }
    }
}
