using DTOs;
using Library.BusinessLogic.Entities;
using Library.BusinessLogic.Interfaces;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.IMantenimientosUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio.BussinesLogic.CasosDeUso.MantenimientosUC
{
    public class AgregarMantenimientoUC : IAgregarMantenimientoUC
    {
        private IMantenimientoRepository _MantenimientoRepository;
        public AgregarMantenimientoUC(IMantenimientoRepository mantenimientoRepository)
        {
            _MantenimientoRepository = mantenimientoRepository;
        }

        public void Add(MantenimientoDTO obj)
        {
            try
            {
                Mantenimiento m = new Mantenimiento(); 
                m.mantenimientoId = obj.mantenimientoId;
                m.Habitacion = obj.Habitacion;
                m.cabaña = obj.cabaña;
                m.FechaMantenimiento = obj.FechaMantenimiento;
                m.Descripcion = obj.Descripcion;
                m.Costo = obj.Costo;
                m.NombreTrabajador = obj.NombreTrabajador;
                _MantenimientoRepository.Add(m);
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
