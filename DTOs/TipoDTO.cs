using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.BusinessLogic.Entities;

namespace DTOs
{
    public class TipoDTO
    {
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public decimal costoXHuesped { get; set; }
        public TipoDTO(Tipo t) 
        {
            this.nombre = t.nombre;
            this.descripcion = t.descripcion;
            this.costoXHuesped = t.costoXHuesped;   
        }
    }
}
