﻿using DTOs;
using Library.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio.BussinesLogic.InterfacesCasosDeUso.ITiposUC
{
    public interface IObtenerTipoPorNombreUC
    {
        public TipoDTO getByName(string n);
    }
}
