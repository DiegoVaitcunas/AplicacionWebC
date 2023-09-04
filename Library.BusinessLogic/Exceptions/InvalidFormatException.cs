using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogic.Exceptions
{
    public class InvalidFormatException : Exception
    {
        public InvalidFormatException() { }

        public InvalidFormatException(string mensaje)
            : base(mensaje) { }
        public InvalidFormatException(string mensaje, Exception ex)
            : base(mensaje, ex) { }
    }
}
