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
    public class ObtenerTipoPorNombreUC : IObtenerTipoPorNombreUC
    {
        private ITipoRepository repo;
        public ObtenerTipoPorNombreUC(ITipoRepository repo)
        {
            this.repo = repo;
        }

        public TipoDTO getByName(string n)
        {
            try
            {
                Tipo aux = repo.getByString(n);
                TipoDTO ret = new TipoDTO(aux);
                return ret;
            }catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
