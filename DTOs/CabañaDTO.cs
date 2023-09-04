using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.BusinessLogic.Entities.ValueObjets;
using Library.BusinessLogic.Entities;

namespace DTOs
{
    public class CabañaDTO
    {
        public CabañaDTO(Cabaña c)
        {
            this.numeroHabitacion = c.numeroHabitacion;
            this.nombreTipo = c.nombreTipo;
            this.nombreTipo = c.nombreTipo;
            this.habilitada = c.habilitada;
            this.nombre = c.nombre;
            this.TipoCabaña = c.TipoCabaña;
            this.descripcion = c.descripcion;
            this.poseeJacuzzi = c.poseeJacuzzi;
            this.capacidad = c.capacidad;
            this.Fotos = c.Fotos;
        }
        public CabañaDTO()
        { }
        public int numeroHabitacion { get; set; }
        public string nombreTipo { get; set; }
        public bool habilitada { get; set; }
        public NombreCabaña nombre { get; set; }
        public Tipo TipoCabaña { get; set; }
        public string descripcion { get; set; }
        public bool poseeJacuzzi { get; set; }
        public int capacidad { get; set; }
        public FotoCabaña Fotos { get; set; }
    }
}
