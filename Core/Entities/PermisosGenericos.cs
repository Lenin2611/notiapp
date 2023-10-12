using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class PermisosGenericos : BaseEntity
{
    public string NombrePermiso { get; set; }
    public ICollection<GenericoVsSubmodulos> GenericoVsSubmodulos { get; set; }
}
