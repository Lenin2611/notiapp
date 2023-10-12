using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PermisosGenericosRepository : GenericRepository<PermisosGenericos>,IPermisosGenericos
{
    private readonly NotiAppContext _context;

    public PermisosGenericosRepository(NotiAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<PermisosGenericos>> GetAllAsync()
    {
        return await _context.PermisosGenericos
        .Include(h => h.GenericoVsSubmodulos)
        .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<PermisosGenericos> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.PermisosGenericos as IQueryable<PermisosGenericos>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombrePermiso.ToLower().Contains(search)); // If necesary add .ToString() after varQuery
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
