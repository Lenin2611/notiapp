using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class MaestrosVsSubmodulos : BaseEntity
{
    public int IdMaestro { get; set; }
    public ModuloMaestros ModuloMaestros { get; set; }
    public int IdSubmodulos { get; set; }
    public Submodulos Submodulos { get; set; }
    public ICollection<GenericoVsSubmodulos> GenericoVsSubmodulos { get; set; }
}
