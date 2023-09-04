using DTOs;
using Library.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio.BussinesLogic.InterfacesCasosDeUso.ISistemaUC.ICabañaUCSistema
{
    public interface IObtenerCabañaPorId
    {
        public CabañaDTO GetById(int id);
    }
}
