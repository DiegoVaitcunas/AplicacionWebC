using Library.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogic.Entities
{
    public class Tipo : IValidable
    {

        [Key]
        public string nombre { get; set; }

        [MinLength(10), MaxLength(200), Required, Editable(true)]
        public string descripcion { get; set; }

        [Required, Editable(true), Column(TypeName = "decimal(18,4)"), Display(Name = "Precio por persona")]
        public decimal costoXHuesped { get; set; }

        #region utilities
        public Tipo(decimal costoXHuesped, string descripcion)
        {
            this.costoXHuesped = costoXHuesped;
            this.descripcion = descripcion;
        }

        public Tipo() { }

        public void Validation(IConfiguracionRepository config)
        {
            if (costoXHuesped <= config.GetInferiorByName("costoXHuesped")) throw new InvalidDataException("El costo por huesped no puede ser igual o menor a 0");
        }
        #endregion
    }
}