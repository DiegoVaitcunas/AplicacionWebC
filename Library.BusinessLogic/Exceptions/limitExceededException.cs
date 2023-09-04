using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogic.Exceptions
{
    public class limitExceededException : Exception
    {
        public limitExceededException() { }

        public limitExceededException(string mensaje)
            : base(mensaje) { }
        public limitExceededException(string mensaje, Exception ex)
            : base(mensaje, ex) { }
    }
}
