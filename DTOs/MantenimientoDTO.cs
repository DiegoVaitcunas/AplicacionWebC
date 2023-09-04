using Library.BusinessLogic.Entities.ValueObjets;
using Library.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class MantenimientoDTO
    {
        public int mantenimientoId { get; set; }
        public int Habitacion { get; set; }
        public Cabaña cabaña { get; set; }
        public FechaMantenimiento FechaMantenimiento { get; set; }
        public string Descripcion { get; set; }
        public double Costo { get; set; }
        public string NombreTrabajador { get; set; }
        public MantenimientoDTO(Mantenimiento m)
        {
            this.mantenimientoId = m.mantenimientoId;
            this.Habitacion = m.Habitacion;
            this.cabaña = m.cabaña;
            this.FechaMantenimiento = m.FechaMantenimiento;
            this.Descripcion = m.Descripcion;
            this.Costo = m.Costo;
            this.NombreTrabajador = m.NombreTrabajador;
        }
    }
}
