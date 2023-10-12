using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces;

public interface IUnitOfWork
{
    public IAuditoria Auditorias { get; }
    public IBlockChain BlockChains { get; }
    public IEstadoNotificacion EstadoNotificaciones { get; }
    public IFormatos Formatos { get; }
    public IGenericoVsSubmodulos GenericoVsSubmodulos { get; }
    public IHiloRespuestaNotificacion HiloRespuestaNotificaciones { get; }
    public IMaestrosVsSubmodulos MaestrosVsSubmodulos { get; }
    public IModuloMaestros ModuloMaestros { get; }
    public IModuloNotificacion ModuloNotificaciones { get; }
    public IPermisosGenericos PermisosGenericos { get; }
    public IRadicados Radicados { get; }
    public IRol Roles { get; }
    public IRolVsMaestro RolVsMaestros { get; }
    public ISubmodulos Submodulos { get; }
    public ITipoNotificaciones TipoNotificaciones { get; }
    public ITipoRequerimiento TipoRequerimientos { get; }

    Task<int> SaveAsync();
}