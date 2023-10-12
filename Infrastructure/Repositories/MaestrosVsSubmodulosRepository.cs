using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MaestrosVsSubmodulosRepository : GenericRepository<MaestrosVsSubmodulos>,IMaestrosVsSubmodulos
{
    private readonly NotiAppContext _context;

    public MaestrosVsSubmodulosRepository(NotiAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<MaestrosVsSubmodulos>> GetAllAsync()
    {
        return await _context.MaestrosVsSubmodulos
        .Include(h => h.GenericoVsSubmodulos)
        .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<MaestrosVsSubmodulos> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.MaestrosVsSubmodulos as IQueryable<MaestrosVsSubmodulos>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Id.ToString().ToLower().Contains(search)); // If necesary add .ToString() after varQuery
        }
        query = query.OrderBy(p => p.Id);
    
        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Include(h => h.GenericoVsSubmodulos)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}
