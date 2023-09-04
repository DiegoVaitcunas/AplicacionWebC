using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogic.Exceptions
{
    public class ObjetNotFoundException : Exception
    {
        public ObjetNotFoundException() { }

        public ObjetNotFoundException(string mensaje)
            : base(mensaje) { }
        public ObjetNotFoundException(string mensaje, Exception ex)
            : base(mensaje, ex) { }
    }
}
