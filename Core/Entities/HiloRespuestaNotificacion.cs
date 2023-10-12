using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class HiloRespuestaNotificacion : BaseEntity
{
    public string NombreTipo { get; set; }
    public ICollection<ModuloNotificacion> ModuloNotificaciones { get; set; }
    public ICollection<BlockChain> BlockChains { get; set; }
}
