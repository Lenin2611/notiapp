# **NotiApp CodeFirst**

- Creación de Proyecto

  1. [Creación de sln](#Creacion-de-sln)

  2. [Creación de proyectos de classlib](#Creacion-de-proyectos-de-classlib)

  3. [Creación de proyecto de webapi](#Creacion-de-proyecto-de-webapi)

  4. [Agregar proyectos al sln](#Agregar-proyectos-al-sln)

  5. [Agregar referencia entre proyectos](#Agregar-referencia-entre-proyectos)

     

- Instalación de paquetes

  1. [Proyecto API](#Proyecto-API)

  2. [Proyecto Infrastructure](#Proyecto-Infrastructure)

     

- API

  1. Controllers

     - [EntityController.cs](#EntityController)
     - [BaseController.cs](#BaseController)

  2. Dtos

     - [EntityDto.cs](#EntityDto)

  3. Extensions

     - [ApplicationServicesExtension.cs](#ApplicationServicesExtension)

  4. Helper

     - [Pager.cs](#Pager)
     - [Params.cs](#Params)

  5. Program

     - [Program.cs](#Program)

       

- Core

  1. Entities
     - [Entity.cs](#Entity)
     - [BaseEntity.cs](#BaseEntity)

  2. Interfaces

     - [IEntity.cs](#IEntity)

     - [IGenericRepository.cs](#IGenericRepository)

     - [IUnitOfWork.cs](#IUnitOfWork)

     

- Infrastructure

  1. Data
     - Configuration
       - [EntityConfiguration.cs](#EntityConfiguration)
     - [DbContext.cs](#DbContext)
  2. Repositories
     - [EntityRepository.cs](#EntityRepository)
     - [GenericRepository.cs](#GenericRepository)
  3. UnitOfWork
     - [UnitOfWork.cs](#UnitOfWork)

## Creación de proyecto

#### Creacion de sln

```
dotnet new sln
```

#### Creacion  de proyectos classlib

```
dotnet new classlib -o Core
dotnet new classlib -o Infrastructure
```

#### Creacion  de proyecto webapi

```
dotnet new webapi -o API
```

#### Agregar proyectos al sln

```
dotnet sln add ApiAnimals
dotnet sln add Core
dotnet sln add Infrastructure
```

#### Agregar referencia entre proyectos

```
cd ./API/
dotnet add reference ../Infrastructure/
cd ..
cd ./Infrastructure/
dotnet add reference ../Core/
```



## Instalacion de paquetes

#### Proyecto API

```
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 7.0.11
dotnet add package Microsoft.EntityFrameworkCore --version 7.0.11
dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.11
dotnet add package Microsoft.Extensions.DependencyInjection --version 7.0.0
dotnet add package System.IdentityModel.Tokens.Jwt --version 6.32.3
dotnet add package Serilog.AspNetCore --version 7.0.0
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 12.0.1
```

#### Proyecto Infrastructure

```
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 7.0.0
dotnet add package Microsoft.EntityFrameworkCore --version 7.0.11
dotnet add package CsvHelper --version 30.0.1
```

#### 

## API

#### Controllers

###### EntityController

```csharp
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class EntityController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EntityController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<IEnumerable<EntityDto>>> Get()
	{
 		var results = await _unitOfWork.Entities.GetAllAsync();
  		return _mapper.Map<List<EntityDto>>(results);
	}

    [HttpGet("{Id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<EntityDto>> Get(int Id)
	{
    	var result = await _unitOfWork.Entities.GetByIdAsync(Id);
    	if (result == null)
    	{
        	return NotFound();
    	}
    	return _mapper.Map<EntityDto>(result);
	}

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EntityDto>> Post(EntityDto resultDto)
    {
    	var result = _mapper.Map<Entity>(resultDto);
        if (auditoriaDto.FechaCreacion == DateOnly.MinValue)
        {
            auditoriaDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            auditoria.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (auditoriaDto.FechaModificacion == DateOnly.MinValue)
        {
            auditoriaDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            auditoria.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.Entities.Add(result);
        await _unitOfWork.SaveAsync();
        if (result == null)
        {
            return BadRequest();
        }
    	resultDto.Id = result.Id
        return CreatedAtAction(nameof(Post), new { id = resultDto.Id }, resultDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EntityDto>> Put(int id, [FromBody] EntityDto resultDto)
    {
        if (resultDto.Id == 0)
        {
            resultDto.Id = id;
        }
        if (resultDto.Id != id)
        {
            return NotFound();
        }
        var result = _mapper.Map<Entity>(resultDto);
        if (auditoriaDto.FechaCreacion == DateOnly.MinValue)
        {
            auditoriaDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            auditoria.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (auditoriaDto.FechaModificacion == DateOnly.MinValue)
        {
            auditoriaDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            auditoria.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        resultDto.Id = result.Id
        _unitOfWork.Entities.Update(result);
        await _unitOfWork.SaveAsync();
       

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _unitOfWork.Entities.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.Entities.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
```

###### BaseController

```csharp
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{

}
```

#### Dtos

###### EntityDto

```csharp
namespace API.Dtos;

public class BlockChainDto
{
    public int Id { get; set; }
    public string HashGenerado { get; set; }
    public DateOnly FechaCreacion { get; set; }
    public DateOnly FechaModificacion { get; set; }
    public int IdTipoNotificacion { get; set; }
    public int IdHiloRespuesta { get; set; }
    public int IdAuditoria { get; set; }
}
```

#### Extensions

###### ApplicationServiceExtension

```csharp
using AspNetCoreRateLimit;
using Core.Interfaces;
using Infrastructure.UnitOfWork;

namespace API.Extensions;

public static class ApplicationServiceExtension
{
    public static void ConfigureCors(this IServiceCollection services) => services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", builder =>
        {
            builder.AllowAnyOrigin() // WithOrigins("https://domain.com")
            .AllowAnyMethod() // WithMethods("GET", "POST")
            .AllowAnyHeader(); // WithHeaders("accept", "content-type")
        });
    });

    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void ConfigureRateLimiting(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddInMemoryRateLimiting();
        services.Configure<IpRateLimitOptions>(options =>
        {
            options.EnableEndpointRateLimiting = true;
            options.StackBlockedRequests = false;
            options.HttpStatusCode = 429;
            options.RealIpHeader = "X-Real-IP";
            options.GeneralRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint = "*",
                    Period = "10s",
                    Limit = 2
                }
            };
        });
    }
}
```

#### Helper

###### Pager

```csharp
namespace ApiAnimals.Helpers;

public class Pager<T> where T : class
    {
    public string Search { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    public List<T> Registers { get; private set; }

    public Pager()
    {
    }

    public Pager(List<T> registers, int total, int pageIndex, int pageSize, string search)
    {
        Registers = registers;
        Total = total;
        PageIndex = pageIndex;
        PageSize = pageSize;
        Search = search;
    }

    public int TotalPages
    {
        get { return (int)Math.Ceiling(Total / (double)PageSize); }
        set { this.TotalPages = value; }
    }

    public bool HasPreviousPage
    {
        get { return (PageIndex > 1); }
        set { this.HasPreviousPage = value; }
    }

    public bool HasNextPage
    {
        get { return (PageIndex < TotalPages); }
        set { this.HasNextPage = value; }
    }
}
```

###### Params

```csharp
namespace ApiAnimals.Helpers;

public class Params
{
    private int _pageSize = 5;
    private const int MaxPageSize = 50;
    private int _pageIndex = 1;
    private string _search;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    public int PageIndex
    {
        get => _pageIndex;
        set => _pageIndex = (value <= 0) ? 1 : value;
    }

    public string Search
    {
        get => _search;
        set => _search = (!String.IsNullOrEmpty(value)) ? value.ToLower() : "";
    }
}
```

#### Program

###### Program

```csharp
using System.Reflection;
using API.Extensions;
using AspNetCoreRateLimit;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<NotiAppContext>(optionsBuilder =>
{
    string connectionString = builder.Configuration.GetConnectionString("MySqlConex");
    optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.ConfigureCors();

builder.Services.AddApplicationServices();

builder.Services.ConfigureRateLimiting();

builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseIpRateLimiting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
```



## Core

#### Entities

###### Entity

```csharp
namespace Core.Entities;

public class BlockChain : BaseEntity
{
    public string HashGenerado { get; set; }
    public int IdTipoNotificacion { get; set; }
    public TipoNotificaciones TipoNotificaciones { get; set; }
    public int IdHiloRespuesta { get; set; }
    public HiloRespuestaNotificacion HiloRespuestas { get; set; }
    public int IdAuditoria { get; set; }
    public Auditoria Auditorias { get; set; }
}
```

###### BaseEntity

```csharp
namespace Core.Entities;

public class BaseEntity
{
    public int/string Id { get; set; }
    public DateOnly FechaCreacion { get; set; }
    public DateOnly FechaModificacion { get; set; }
}

```

#### 

#### Interface

###### IEntity

```csharp
using Core.Entities;

namespace Core.Interfaces;

public interface IBlockChain : IGenericRepository<BlockChain>
{

}
```

###### IGenericRepository

```csharp
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int/string Id);
    Task<IEnumerable<T>> GetAllAsync();
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync(int pageIndex, int pageSize, string search);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    void Update(T entity);
}
```

###### IUnitOfWork

```csharp
namespace Core.Interfaces;

public interface IUnitOfWork
{
    public IBlockChain BlockChains { get; }
    ...

    Task<int> SaveAsync();
}
```

###### 

## Infrastructure

#### Data

##### Configuration

###### EntityConfiguration

```csharp
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class BlockChainConfiguration : IEntityTypeConfiguration<BlockChain>
{
    public void Configure(EntityTypeBuilder<BlockChain> builder)
    {
        builder.ToTable("blockchain");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);

        builder.Property(x => x.HashGenerado).IsRequired().HasMaxLength(100);

        builder.Property(x => x.FechaCreacion).HasColumnType("date");

        builder.Property(x => x.FechaModificacion).HasColumnType("date");

        builder.Property(x => x.IdTipoNotificacion).HasColumnType("int");
        builder.HasOne(x => x.TipoNotificaciones).WithMany(x => x.BlockChains).HasForeignKey(x => x.IdTipoNotificacion);
        
        builder.Property(x => x.IdHiloRespuesta).HasColumnType("int");
        builder.HasOne(x => x.HiloRespuestas).WithMany(x => x.BlockChains).HasForeignKey(x => x.IdHiloRespuesta);
        
        builder.Property(x => x.IdAuditoria).HasColumnType("int");
        builder.HasOne(x => x.Auditorias).WithMany(x => x.BlockChains).HasForeignKey(x => x.IdAuditoria);
    }
}
```

###### DbContext

```csharp
using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class NotiAppContext : DbContext
{
    public NotiAppContext(DbContextOptions options) : base(options)
    {
    }

    // DbSets
    public DbSet<BlockChain> BlockChains { get; set; }
    ...

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
```

#### Configuration

###### EntityConfiguration

```csharp
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BlockChainRepository : GenericRepository<BlockChain>,IBlockChain
{
    private readonly NotiAppContext _context;

    public BlockChainRepository(NotiAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<BlockChain>> GetAllAsync()
    {
        return await _context.BlockChains.ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<BlockChain> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.BlockChains as IQueryable<BlockChain>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.HashGenerado.ToLower().Contains(search)); // If necesary add .ToString() after varQuery
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
```

###### GenericRepository

```csharp
using System.Linq.Expressions;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly NotiAppContext _context;

    public GenericRepository(NotiAppContext context)
    {
        _context = context;
    }

    public virtual void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public virtual void AddRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
    }

    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
        // return (IEnumerable<T>) await _context.Entities.FromSqlRaw("SELECT * FROM entity").ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(int/string id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public virtual void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }

    public virtual void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }
    public virtual async Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string _search
    )
    {
        var totalRegistros = await _context.Set<T>().CountAsync();
        var registros = await _context
            .Set<T>()
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (totalRegistros, registros);
    }
}
```

#### UnitOfWork

###### UnitOfWork

```csharp
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork,IDisposable
{
    private readonly NotiAppContext _context;
    private IBlockChain _BlockChains;
    ...

    public UnitOfWork(NotiAppContext context)
    {
        _context = context;
    }

    public IBlockChain BlockChains
    {
        get
        {
            if (_BlockChains == null)
            {
                _BlockChains = new BlockChainRepository(_context);
            }
            return _BlockChains;
        }
    }
    ...

    public Task<int> SaveAsync()
    {
        return _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

```

###### 