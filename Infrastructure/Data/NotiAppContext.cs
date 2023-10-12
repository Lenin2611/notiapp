using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class NotiAppContext : DbContext
{
    public NotiAppContext(DbContextOptions options) : base(options)
    {
    }

    // DbSets
    public DbSet<Auditoria> Auditorias { get; set; }
    public DbSet<BlockChain> BlockChains { get; set; }
    public DbSet<EstadoNotificacion> EstadoNotificaciones { get; set; }
    public DbSet<Formatos> Formatos { get; set; }
    public DbSet<GenericoVsSubmodulos> GenericoVsSubmodulos { get; set; }
    public DbSet<HiloRespuestaNotificacion> HiloRespuestaNotificaciones { get; set; }
    public DbSet<MaestrosVsSubmodulos> MaestrosVsSubmodulos { get; set; }
    public DbSet<ModuloMaestros> ModuloMaestros { get; set; }
    public DbSet<ModuloNotificacion> ModuloNotificaciones { get; set; }
    public DbSet<PermisosGenericos> PermisosGenericos { get; set; }
    public DbSet<Radicados> Radicados { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<RolVsMaestro> RolVsMaestros { get; set; }
    public DbSet<Submodulos> Submodulos { get; set; }
    public DbSet<TipoNotificaciones> TipoNotificaciones { get; set; }
    public DbSet<TipoRequerimiento> TipoRequerimientos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
