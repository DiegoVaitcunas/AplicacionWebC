using Library.BusinessLogic.Entities.ValueObjets;
using Library.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogic.Entities
{
    public class Mantenimiento : IValidable
    {
        [Key]
        public int mantenimientoId { get; set; }
        [ForeignKey("Cabaña")]
        public int Habitacion { get; set; }
        [Required, NotMapped]
        public Cabaña cabaña { get; set; }
        [Required, Column("FechaMantenimiento")]
        public FechaMantenimiento FechaMantenimiento { get; set; }
        [MinLength(10), MaxLength(200)]
        public string Descripcion { get; set; }
        [Required]
        public double Costo { get; set; }
        [Required]
        public string NombreTrabajador { get; set; }

        #region utilities
        public Mantenimiento() { }
        public Mantenimiento(int habitacion, Cabaña cabaña, DateTime fechaMantenimiento, string descripcion, double costo, string nombreTrabajador)
        {
            FechaMantenimiento = new FechaMantenimiento();
            FechaMantenimiento.Valor = fechaMantenimiento;
            //this.FechaMantenimiento = fechaMantenimiento;
            this.Habitacion = habitacion;
            this.cabaña = cabaña;
            this.Descripcion = descripcion;
            this.Costo = costo;
            this.NombreTrabajador = nombreTrabajador;
        }

        public void Validation(IConfiguracionRepository config)
        {
            if (Descripcion is null || Costo <= config.GetInferiorByName("Costo") || NombreTrabajador is null) throw new InvalidDataException("No puede haber campos vacios");

            if (Habitacion < config.GetInferiorByName("numeroHabitacion")) throw new InvalidDataException("El numero de habitacion no puede ser menor a 0");

            //if (FechaMantenimiento > config.GetSuperiorByNameDate("FechaMantenimiento")) throw new InvalidDataException("La fecha del mantenimiento no puede ser mayor a la de hoy");

            //if (FechaMantenimiento < config.GetInferiorByNameDate("FechaMantenimiento")) throw new InvalidDataException("La fecha del mantenimiento no puede ser inferior a " + config.GetInferiorByNameDate("FechaMantenimiento"));

            //if (Costo <= config.GetInferiorByName("Costo")) throw new InvalidDataException("El costo de un mantenimiento no puede ser menor a 0");

            FechaMantenimiento.Validation(config);
        }
        #endregion
    }
}
