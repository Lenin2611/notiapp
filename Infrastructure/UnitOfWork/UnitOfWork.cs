using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork,IDisposable
{
    private readonly NotiAppContext _context;
    private IAuditoria _Auditorias;
    private IBlockChain _BlockChains;
    private IEstadoNotificacion _EstadoNotificaciones;
    private IFormatos _Formatos;
    private IGenericoVsSubmodulos _GenericoVsSubmodulos;
    private IHiloRespuestaNotificacion _HiloRespuestaNotificaciones;
    private IMaestrosVsSubmodulos _MaestrosVsSubmodulos;
    private IModuloMaestros _ModuloMaestros;
    private IModuloNotificacion _ModuloNotificaciones;
    private IPermisosGenericos _PermisosGenericos;
    private IRadicados _Radicados;
    private IRol _Roles;
    private IRolVsMaestro _RolVsMaestros;
    private ISubmodulos _Submodulos;
    private ITipoNotificaciones _TipoNotificaciones;
    private ITipoRequerimiento _TipoRequerimientos;

    public UnitOfWork(NotiAppContext context)
    {
        _context = context;
    }

    public IAuditoria Auditorias
    {
        get
        {
            if (_Auditorias == null)
            {
                _Auditorias = new AuditoriaRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _Auditorias;
        }
    }

    public IBlockChain BlockChains
    {
        get
        {
            if (_BlockChains == null)
            {
                _BlockChains = new BlockChainRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _BlockChains;
        }
    }

    public IEstadoNotificacion EstadoNotificaciones
    {
        get
        {
            if (_EstadoNotificaciones == null)
            {
                _EstadoNotificaciones = new EstadoNotificacionRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _EstadoNotificaciones;
        }
    }

    public IFormatos Formatos
    {
        get
        {
            if (_Formatos == null)
            {
                _Formatos = new FormatosRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _Formatos;
        }
    }

    public IGenericoVsSubmodulos GenericoVsSubmodulos
    {
        get
        {
            if (_GenericoVsSubmodulos == null)
            {
                _GenericoVsSubmodulos = new GenericoVsSubmodulosRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _GenericoVsSubmodulos;
        }
    }

    public IHiloRespuestaNotificacion HiloRespuestaNotificaciones
    {
        get
        {
            if (_HiloRespuestaNotificaciones == null)
            {
                _HiloRespuestaNotificaciones = new HiloRespuestaNotificacionRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _HiloRespuestaNotificaciones;
        }
    }

    public IMaestrosVsSubmodulos MaestrosVsSubmodulos
    {
        get
        {
            if (_MaestrosVsSubmodulos == null)
            {
                _MaestrosVsSubmodulos = new MaestrosVsSubmodulosRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _MaestrosVsSubmodulos;
        }
    }

    public IModuloMaestros ModuloMaestros
    {
        get
        {
            if (_ModuloMaestros == null)
            {
                _ModuloMaestros = new ModuloMaestrosRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _ModuloMaestros;
        }
    }

    public IModuloNotificacion ModuloNotificaciones
    {
        get
        {
            if (_ModuloNotificaciones == null)
            {
                _ModuloNotificaciones = new ModuloNotificacionRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _ModuloNotificaciones;
        }
    }

    public IPermisosGenericos PermisosGenericos
    {
        get
        {
            if (_PermisosGenericos == null)
            {
                _PermisosGenericos = new PermisosGenericosRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _PermisosGenericos;
        }
    }

    public IRadicados Radicados
    {
        get
        {
            if (_Radicados == null)
            {
                _Radicados = new RadicadosRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _Radicados;
        }
    }

    public IRol Roles
    {
        get
        {
            if (_Roles == null)
            {
                _Roles = new RolRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _Roles;
        }
    }

    public IRolVsMaestro RolVsMaestros
    {
        get
        {
            if (_RolVsMaestros == null)
            {
                _RolVsMaestros = new RolVsMaestroRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _RolVsMaestros;
        }
    }

    public ISubmodulos Submodulos
    {
        get
        {
            if (_Submodulos == null)
            {
                _Submodulos = new SubmodulosRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _Submodulos;
        }
    }

    public ITipoNotificaciones TipoNotificaciones
    {
        get
        {
            if (_TipoNotificaciones == null)
            {
                _TipoNotificaciones = new TipoNotificacionesRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _TipoNotificaciones;
        }
    }

    public ITipoRequerimiento TipoRequerimientos
    {
        get
        {
            if (_TipoRequerimientos == null)
            {
                _TipoRequerimientos = new TipoRequerimientoRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _TipoRequerimientos;
        }
    }

    public Task<int> SaveAsync()
    {
        return _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
