using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AuditoriaRepository : GenericRepository<Auditoria>,IAuditoria
{
    private readonly NotiAppContext _context;

    public AuditoriaRepository(NotiAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Auditoria>> GetAllAsync()
    {
        return await _context.Auditorias
                    .Include(c => c.BlockChains)
                    .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Auditoria> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.Auditorias as IQueryable<Auditoria>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreUsuario.ToLower().Contains(search)); // If necesary add .ToString() after varQuery
        }
        query = query.OrderBy(p => p.Id);
    
        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Include(p => p.BlockChains)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}