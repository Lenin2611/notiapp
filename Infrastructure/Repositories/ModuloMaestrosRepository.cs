using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ModuloMaestrosRepository : GenericRepository<ModuloMaestros>,IModuloMaestros
{
    private readonly NotiAppContext _context;

    public ModuloMaestrosRepository(NotiAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<ModuloMaestros>> GetAllAsync()
    {
        return await _context.ModuloMaestros
        .Include(h => h.MaestrosVsSubmodulos)
        .Include(h => h.RolVsMaestros)
        .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<ModuloMaestros> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.ModuloMaestros as IQueryable<ModuloMaestros>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreModulo.ToLower().Contains(search)); // If necesary add .ToString() after varQuery
        }
        query = query.OrderBy(p => p.Id);
    
        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Include(h => h.MaestrosVsSubmodulos)
                        .Include(h => h.RolVsMaestros)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}
