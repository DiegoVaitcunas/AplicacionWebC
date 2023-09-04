using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogic.Exceptions
{
    public class InUseException : Exception
    {
        public InUseException() { }

        public InUseException(string mensaje)
            : base(mensaje) { }
        public InUseException(string mensaje, Exception ex)
            : base(mensaje, ex) { }
    }
}
