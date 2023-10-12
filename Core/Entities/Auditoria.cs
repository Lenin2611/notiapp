using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class Auditoria : BaseEntity
{
    public string NombreUsuario { get; set; }
    public int DescAccion { get; set; }
    public ICollection<BlockChain> BlockChains { get; set; }
}
