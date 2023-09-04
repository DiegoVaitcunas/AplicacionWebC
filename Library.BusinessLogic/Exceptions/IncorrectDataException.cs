using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogic.Exceptions
{
    public class IncorrectDataException : Exception
    {
        public IncorrectDataException() { }

        public IncorrectDataException(string mensaje)
            : base(mensaje) { }
        public IncorrectDataException(string mensaje, Exception ex)
            : base(mensaje, ex) { }
    }
}
