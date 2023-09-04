using Library.BusinessLogic.Entities;
using Library.BusinessLogic.Exceptions;
using Library.BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.AccesData.Memory
{
    public class SQLMantenimientoRepostory : IMantenimientoRepository
    {
        public ObligatorioContext context { get; set; }
        public SQLMantenimientoRepostory()
        {
            context = new ObligatorioContext();
        }
        public void Add(Mantenimiento obj)
        {
            var mantenimientosXDia = context.mantenimientos.Where(m => m.FechaMantenimiento.Valor == obj.FechaMantenimiento.Valor && m.Habitacion == obj.Habitacion).ToList();
            if (mantenimientosXDia.Count() < 3)
            {
                try
                {
                    obj.Validation(new SQLConfiguracionRepository());
                    context.mantenimientos.Add(obj);
                    context.SaveChanges();
                }
                catch (InvalidDataException e)
                {
                    throw e;
                }
                catch (Exception)
                {
                    throw new Exception("Ocurrio un error inesperado");
                }
            }
            else
            {
                throw new limitExceededException("Se supero el limite de mantenimientos");
            }

        }

        public int Count()
        {
            return context.cabañas.Count();
        }

        public IEnumerable<Mantenimiento> GetAll()
        {
            return context.mantenimientos;
        }

        public Mantenimiento GetById(int id)
        {
            Mantenimiento ret = context.mantenimientos.Where(m => m.mantenimientoId == id).FirstOrDefault();
            if (ret != null) return context.mantenimientos.Where(m => m.mantenimientoId == id).FirstOrDefault();
            throw new ObjetNotFoundException("No se encontro un tipo con ese id");
        }
        public void delete(Mantenimiento obj)
        {
            context.mantenimientos.Remove(obj);
            context.SaveChanges();
        }

        public IEnumerable<Mantenimiento> ListarMantenimientosDeCabana(int id, DateTime fechaI, DateTime fechaF)
        {
            if (fechaF > fechaI)
            {
                var aux = context.mantenimientos.Where(m => m.FechaMantenimiento.Valor >= fechaI && m.FechaMantenimiento.Valor <= fechaF && m.Habitacion == id).OrderByDescending(m => m.Costo);
                if (aux.Count() > 0)
                {
                    return aux;
                }
                throw new IsNotFoundException("No se encontraron mantenimientos para esa caba");
            }
            throw new InvalidDataException("La fecha inicial no puede ser mayor a la final");
        }

        /***************************************************************************************************/
        /***************************************Falta Terminar**********************************************/
        /***************************************************************************************************/

        public IEnumerable<Mantenimiento> GetMantenimientosDeCabañasEntreCapacidades(int cap1, int cap2)
        {
            var ret = GetMantenimientosConCabaña().Where(m => m.cabaña.capacidad >= cap1 && m.cabaña.capacidad <= cap2)
                .GroupBy(m => m.NombreTrabajador).Select(Mnuevo => new Mantenimiento
				{
                    NombreTrabajador = Mnuevo.Key,
					Costo = Mnuevo.Sum(m => m.Costo)
                });

            if (ret.Count() > 0) return ret;
            throw new IsNotFoundException("No se encontro un mantenimiento para una cabaña entre esas capacidades");
        }
        public IEnumerable<Mantenimiento> GetMantenimientosConCabaña ()
		{
			var mantenimientos = context.mantenimientos.AsNoTracking()
				.Select(m => new Mantenimiento
				{
					cabaña = context.cabañas.FirstOrDefault(c => c.numeroHabitacion == m.Habitacion),
					NombreTrabajador = m.NombreTrabajador,
					mantenimientoId = m.mantenimientoId,
					Habitacion = m.Habitacion,
					FechaMantenimiento = m.FechaMantenimiento,
					Costo = m.Costo,
					Descripcion = m.Descripcion
				});

			if (mantenimientos.Count() > 0)
			{
				return mantenimientos;
			}

			throw new IsNotFoundException("No se pueden acceder a los datos porque el mantenimiento no tiene una cabaña identificada");
		}
	}
}
