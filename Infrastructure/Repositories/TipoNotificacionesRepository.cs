using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TipoNotificacionesRepository : GenericRepository<TipoNotificaciones>,ITipoNotificaciones
{
    private readonly NotiAppContext _context;

    public TipoNotificacionesRepository(NotiAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<TipoNotificaciones>> GetAllAsync()
    {
        return await _context.TipoNotificaciones
        .Include(h => h.ModuloNotificaciones)
        .Include(h => h.BlockChains)
        .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<TipoNotificaciones> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.TipoNotificaciones as IQueryable<TipoNotificaciones>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreTipo.ToLower().Contains(search)); // If necesary add .ToString() after varQuery
        }
        query = query.OrderBy(p => p.Id);
    
        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Include(h => h.ModuloNotificaciones)
                        .Include(h => h.BlockChains)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}
