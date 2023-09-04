using DTOs;
using Library.BusinessLogic.Entities;
using Library.BusinessLogic.Interfaces;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ICabañasUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio.BussinesLogic.CasosDeUso.CabañasUC
{
    public class RegistrarCabañaUC : IRegistrarCabañaUC
    {
        private ICabanaRepository _CabanaRepository;
        public RegistrarCabañaUC(ICabanaRepository cabanaRepository)
        {
            _CabanaRepository = cabanaRepository;
        }

        public void Add(CabañaDTO c)
        {
            try
            {
                Cabaña obj = new Cabaña();
                obj.numeroHabitacion = c.numeroHabitacion;
                obj.nombre = c.nombre;
                obj.descripcion = c.descripcion;
                obj.capacidad = c.capacidad;
                obj.poseeJacuzzi = c.poseeJacuzzi;
                obj.habilitada = c.habilitada;
                obj.Fotos = c.Fotos;
                obj.nombreTipo = c.nombreTipo;
                obj.TipoCabaña = c.TipoCabaña;
                _CabanaRepository.Add(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
