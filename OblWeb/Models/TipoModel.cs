using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ObligatorioP3WebApplication.Models
{
    public class TipoModel
    {
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public decimal costoXHuesped { get; set; }
        public TipoModel() { }
    }
}
