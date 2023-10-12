using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ModuloNotificacionRepository : GenericRepository<ModuloNotificacion>,IModuloNotificacion
{
    private readonly NotiAppContext _context;

    public ModuloNotificacionRepository(NotiAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<ModuloNotificacion>> GetAllAsync()
    {
        return await _context.ModuloNotificaciones.ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<ModuloNotificacion> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.ModuloNotificaciones as IQueryable<ModuloNotificacion>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.AsuntoNotificacion.ToLower().Contains(search)); // If necesary add .ToString() after varQuery
        }
        query = query.OrderBy(p => p.Id);
    
        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}
