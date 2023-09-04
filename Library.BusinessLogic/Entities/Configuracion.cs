using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogic.Entities;

public class Configuracion
{
    [Key]
    public string Atributo { get; set; }
    public int? LimiteSuperior { get; set; }
    public int? LimiteInferior { get; set; }
    public DateTime? LimiteSuperiorDate { get; set; }
    public DateTime? LimiteInferiorDate { get; set; }
}
