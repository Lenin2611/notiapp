using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EstadoNotificacionRepository : GenericRepository<EstadoNotificacion>,IEstadoNotificacion
{
    private readonly NotiAppContext _context;

    public EstadoNotificacionRepository(NotiAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<EstadoNotificacion>> GetAllAsync()
    {
        return await _context.EstadoNotificaciones
                    .Include(c => c.ModuloNotificaciones)
                    .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<EstadoNotificacion> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.EstadoNotificaciones as IQueryable<EstadoNotificacion>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreEstado.ToLower().Contains(search)); // If necesary add .ToString() after varQuery
        }
        query = query.OrderBy(p => p.Id);
    
        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Include(p => p.ModuloNotificaciones)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}
