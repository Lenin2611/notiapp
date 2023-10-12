using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class BlockChain : BaseEntity
{
    public string HashGenerado { get; set; }
    public int IdTipoNotificacion { get; set; }
    public TipoNotificaciones TipoNotificaciones { get; set; }
    public int IdHiloRespuesta { get; set; }
    public HiloRespuestaNotificacion HiloRespuestas { get; set; }
    public int IdAuditoria { get; set; }
    public Auditoria Auditorias { get; set; }
}
