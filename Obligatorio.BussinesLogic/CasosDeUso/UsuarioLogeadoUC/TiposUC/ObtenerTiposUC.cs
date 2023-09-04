using DTOs;
using Library.BusinessLogic.Entities;
using Library.BusinessLogic.Interfaces;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ITiposUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio.BussinesLogic.CasosDeUso.TiposUC
{
    public class ObtenerTiposUC : IObtenerTiposUC
    {
        private ITipoRepository repo;
        public ObtenerTiposUC(ITipoRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<TipoDTO> GetAll()
        {
            try
            {
                return repo.GetAll().Select(x => new TipoDTO(x));
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
