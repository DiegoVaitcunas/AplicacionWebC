
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ObligatorioP3WebApplication.Models
{
    public class MantenimientoModel
    {
        public int mantenimientoId { get; set; }
        public int Habitacion { get; set; }
        public CabañaModel cabaña { get; set; }
        public FechaMantenimientoModel FechaMantenimiento { get; set; }
        public string Descripcion { get; set; }
        public double Costo { get; set; }
        public string NombreTrabajador { get; set; }
        public MantenimientoModel() { }
    }
    public class FechaMantenimientoModel
    {
        public DateTime Valor { get; set; }
    }
}
