using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SubmodulosRepository : GenericRepository<Submodulos>,ISubmodulos
{
    private readonly NotiAppContext _context;

    public SubmodulosRepository(NotiAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Submodulos>> GetAllAsync()
    {
        return await _context.Submodulos
        .Include(h => h.MaestrosVsSubmodulos)
        .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Submodulos> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.Submodulos as IQueryable<Submodulos>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreSubmodulo.ToLower().Contains(search)); // If necesary add .ToString() after varQuery
        }
        query = query.OrderBy(p => p.Id);
    
        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Include(h => h.MaestrosVsSubmodulos)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}
