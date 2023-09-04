using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ObligatorioP3WebApplication.Models
{
    public class CabañaModel
    {
        public int numeroHabitacion { get; set; }
        public string nombreTipo { get; set; }
        public bool habilitada { get; set; }
        public NombreCabañaModel nombre { get; set; }
        public TipoModel TipoCabaña { get; set; }
        public string descripcion { get; set; }
        public bool poseeJacuzzi { get; set; }
        public int capacidad { get; set; }
        public FotoCabañaModel Fotos { get; set; }
        public CabañaModel() { }

    }
    public class FotoCabañaModel
    {
        public FotoCabañaModel(string valor)
        {
            Valor = valor;
        }
        public string Valor { get; set; }
    }
    public class NombreCabañaModel
    {
        public string Valor { get; set; }
    }
}