using Library.BusinessLogic.Entities.ValueObjets;
using Library.BusinessLogic.Exceptions;
using Library.BusinessLogic.Interfaces;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogic.Entities
{
    public class Cabaña : IValidable
    {
        [Key]
        public int numeroHabitacion { get; set; }
        [ForeignKey("TipoCabaña")]
        public string nombreTipo { get; set; }
        public bool habilitada { get; set; }
        [Required, Column("nombre")]
        public NombreCabaña nombre { get; set; }
        [Required, NotMapped]
        public Tipo TipoCabaña { get; set; }

        [MinLength(10), MaxLength(500)]
        public string descripcion { get; set; }

        [Required]
        public bool poseeJacuzzi { get; set; }

        [Required]
        public int capacidad { get; set; }

        [Required, Column("Fotos")]
        public FotoCabaña Fotos { get; set; }

        #region utilities

        #region Validations

        #region FotoOld
        /*private bool ValidarFoto()
        {
            string extension = "";
            int contador = 000;

            foreach (string f in Fotos)
            {
                contador++;

                for (int i = f.Length - 4; i > f.Length; i++)
                {
                    extension += f[i];
                }
                if (extension != "png" || extension != "jpg")
                {
                    return false;
                }

                f.Replace(".", $"-{contador}.");
            }
            return true;
        }*/
        #endregion

        public void Validation(IConfiguracionRepository config)
        {
            if (this.numeroHabitacion < config.GetInferiorByName("numeroHabitacion")) throw new InvalidFormatException("El numero de habitacion debe ser mayor a 0");

            if (nombre is null || descripcion is null || Fotos is null) throw new IncorrectDataException("Los datos no pueden ser nulos");

            //if (nombre.Length < config.GetInferiorByName("nombre")) throw new InvalidFormatException("El nombre de la cabaña debe ser mayor");

            //if (!nombre.All(c => char.IsLetter(c))) throw new InvalidFormatException("El nombre solo puede contener caracteres alfanumericos");

            //if (nombre.Trim() != nombre) throw new InvalidFormatException("El nombre no debe contener espacios");

            if (capacidad <= config.GetInferiorByName("capacidad")) throw new InvalidFormatException("La capacidad no puede ser menor o igual a 0");
            if (capacidad >= config.GetSuperiorByName("capacidad")) throw new InvalidFormatException("La capacidad no puede ser mayor o igual a " + config.GetSuperiorByName("capacidad"));

            Fotos.Validation(config);

            nombre.Validation(config);
        }

        #endregion

        public Cabaña(string nombre, string descripcion, bool poseeJacuzzi, int capacidad, string foto, bool habilitada)
        {
            this.nombre = new NombreCabaña();
            this.nombre.Valor = nombre;
            this.Fotos = new FotoCabaña();
            this.Fotos.Valor = foto;
            //this.nombre = nombre;
            //this.Fotos = foto;
            this.TipoCabaña = new Tipo();
            this.descripcion = descripcion;
            this.poseeJacuzzi = poseeJacuzzi;
            this.capacidad = capacidad;
            this.habilitada = habilitada;
        }
        public Cabaña() { }

        #endregion
    }
}
