using DTOs;
using Library.BusinessLogic.Entities;
using Library.BusinessLogic.Interfaces;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ICabañasUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Obligatorio.BussinesLogic.CasosDeUso.CabañasUC
{
    public class EliminarCabañaUC : IEliminarCabañaUC
    {
        private ICabanaRepository _CabanaRepository;
        public EliminarCabañaUC(ICabanaRepository cabanaRepository)
        {
            _CabanaRepository = cabanaRepository;
        }
        public void delete(CabañaDTO obj)
        {
            try
            {
                Cabaña ret = new Cabaña();
                ret.capacidad = obj.capacidad;
                ret.TipoCabaña = obj.TipoCabaña;
                ret.habilitada = obj.habilitada;
                ret.poseeJacuzzi = obj.poseeJacuzzi;
                ret.nombreTipo = obj.nombreTipo;
                ret.nombre = obj.nombre;
                ret.Fotos = obj.Fotos;
                ret.numeroHabitacion = obj.numeroHabitacion;
                _CabanaRepository.delete(ret);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
