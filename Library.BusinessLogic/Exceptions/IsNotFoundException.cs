using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogic.Exceptions
{
    public class IsNotFoundException : Exception
    {
        public IsNotFoundException() { }

        public IsNotFoundException(string mensaje)
            : base(mensaje) { }
        public IsNotFoundException(string mensaje, Exception ex)
            : base(mensaje, ex) { }
    }
}
