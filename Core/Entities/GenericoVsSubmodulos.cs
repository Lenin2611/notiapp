using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class GenericoVsSubmodulos : BaseEntity
{
    public int IdGenericos { get; set; }
    public PermisosGenericos PermisosGenericos { get; set; }
    public int IdSubmodulos { get; set; }
    public MaestrosVsSubmodulos MaestrosVsSubmodulos { get; set; }
    public int IdRol { get; set; }
    public Rol Roles { get; set; }
}
