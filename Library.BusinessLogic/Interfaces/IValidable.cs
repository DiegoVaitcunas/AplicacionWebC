﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogic.Interfaces
{
    internal interface IValidable
    {
        public void Validation(IConfiguracionRepository config);
    }
}
