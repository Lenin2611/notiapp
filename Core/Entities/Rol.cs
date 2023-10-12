using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class Rol : BaseEntity
{
    public string Nombre { get; set; }
    public ICollection<GenericoVsSubmodulos> GenericoVsSubmodulos { get; set; }
    public ICollection<RolVsMaestro> RolVsMaestros { get; set; }
}
