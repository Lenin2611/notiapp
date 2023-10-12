using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FormatosRepository : GenericRepository<Formatos>,IFormatos
{
    private readonly NotiAppContext _context;

    public FormatosRepository(NotiAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Formatos>> GetAllAsync()
    {
        return await _context.Formatos
                    .Include(c => c.ModuloNotificaciones)
                    .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Formatos> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.Formatos as IQueryable<Formatos>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreFormato.ToLower().Contains(search)); // If necesary add .ToString() after varQuery
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
