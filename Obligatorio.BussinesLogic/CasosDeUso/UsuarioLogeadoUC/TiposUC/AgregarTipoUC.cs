
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
    public class AgregarTipoUC : IAgregarTipoUC
    {
        private ITipoRepository _TipoRepository;
        public AgregarTipoUC(ITipoRepository tipoRepository)
        {
            _TipoRepository = tipoRepository;
        }

        public void Add(TipoDTO t)
        {
            try
            {
                Tipo obj = new Tipo();
                obj.descripcion = t.descripcion;
                obj.costoXHuesped = t.costoXHuesped;
                obj.nombre = t.nombre;  
                _TipoRepository.Add(obj);
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
