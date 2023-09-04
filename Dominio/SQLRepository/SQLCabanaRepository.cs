using DTOs;
using Library.BusinessLogic.Entities;
using Library.BusinessLogic.Exceptions;
using Library.BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Library.AccesData.Memory
{
    public class SQLCabanaRepository : ICabanaRepository
    {
        public ObligatorioContext context { get; set; }
        public SQLCabanaRepository()
        {
            context = new ObligatorioContext();
        }
        public void Add(Cabaña obj)
        {

            try
            {
                obj.Validation(new SQLConfiguracionRepository());
                //RegisterCabana(obj);
                context.cabañas.Add(obj);
                context.SaveChanges();
            }
            catch (NotImplementedException e)
            {
                throw e;
            }
            catch (InvalidFormatException e)
            {
                throw e;
            }
            catch (InvalidOperationException e)
            {
                throw e;
            }
            catch (IsNotFoundException e)
            {
                throw e;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrio un error inesperado");
            }
        }

        public void RegisterCabana(Cabaña c)
        {
            if (context.cabañas.Any(c1 => c1.numeroHabitacion == c.numeroHabitacion))
            {
                throw new InvalidOperationException("Ya existe una cabaña con ese numero de habitacion");
            }
        }

        public int Count()
        {
            return context.cabañas.Count();
        }

        public IEnumerable<Cabaña> GetAll()
        {
            if (GetCabañasConTipo().Count() > 0) return GetCabañasConTipo();
            throw new IsNotFoundException("No se encontraron cabañas");
        }

        public Cabaña GetById(int id)
        {
            return GetCabañasConTipo().Where(c => c.numeroHabitacion == id).FirstOrDefault();
        }

        public void delete(Cabaña obj)
        {
            context.cabañas.Remove(obj);
            context.SaveChanges();
        }

        public Cabaña getByNombre(string nombre)
        {
            if (GetCabañasConTipo().Where(c => c.nombre.Valor.Contains(nombre)).FirstOrDefault() != null) return GetCabañasConTipo().Where(c => c.nombre.Valor == nombre).FirstOrDefault();
            throw new IsNotFoundException("No se encontro una cabaña con ese nombre");
        }
        public IEnumerable<Cabaña> getByCantidad(int cant)
        {
            if (cant > 0)
            {
                var aux = GetCabañasConTipo().Where(c => c.capacidad <= cant);
                if (aux.Count() > 0) return aux;
                throw new IsNotFoundException("No se encontro una cabaña con esa cantidad");
            }
            throw new InvalidDataException("La cantidad debe ser mayor a 0");
        }
        public IEnumerable<Cabaña> obtenerCabanaPorTipo(Tipo tipo)
        {
            var aux = GetCabañasConTipo().Where(c => c.nombreTipo == tipo.nombre);
            if (aux.Count() > 0) return aux;
            throw new IsNotFoundException("No se encontro una cabaña de ese tipo");
        }

        public IEnumerable<Cabaña> GetHabilitadas()
        {
            var aux = GetCabañasConTipo().Where(c => c.habilitada == true);
            if (aux.Count() > 0) return aux;
            throw new IsNotFoundException("No se encontro una cabaña habilitada");
        }
        public IEnumerable<Cabaña> GetCabañasConTipo()
        {
            var cabañas = context.cabañas.AsNoTracking()
                .Select(c => new Cabaña
                {
                    numeroHabitacion = c.numeroHabitacion,
                    nombre = c.nombre,
                    descripcion = c.descripcion,
                    capacidad = c.capacidad,
                    poseeJacuzzi = c.poseeJacuzzi,
                    habilitada = c.habilitada,
                    Fotos = c.Fotos,
                    nombreTipo = c.nombreTipo,
                    TipoCabaña = context.tipos.FirstOrDefault(t => t.nombre == c.nombreTipo)
                });

            if (cabañas.Count() > 0)
            {
                return cabañas;
            }

            throw new IsNotFoundException("No se pueden acceder a los datos porque la cabana no tiene un tipo identificado");
        }

        public IEnumerable<Cabaña> GetByMontoAndTipo(string nameTipo, int monto)
        {
            /*a. Dados el identificador de un tipo y un monto obtener el nombre y capacidad 
             * (cantidad de huéspedes que puede alojar) de las cabañas de ese tipo que tengan 
             * un costo diario menor a ese monto, que tengan jacuzzi y estén habilitadas para reserva. */

            var aux = GetCabañasConTipo().Where(c => c.nombreTipo == nameTipo && c.TipoCabaña.costoXHuesped < monto && c.poseeJacuzzi && c.habilitada);
            if (aux.Count() > 0) return aux;
            throw new IsNotFoundException("No se encontraron cabañas que esten habilitadas, tengan jacuzzi, de menor costo y con ese tipo");
        }


    }
}
