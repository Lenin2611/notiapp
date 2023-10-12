using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class PermisosGenericosConfiguration : IEntityTypeConfiguration<PermisosGenericos>
{
    public void Configure(EntityTypeBuilder<PermisosGenericos> builder)
    {
        builder.ToTable("permisosgenericos");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);

        builder.Property(x => x.NombrePermiso).IsRequired().HasMaxLength(50);

        builder.Property(x => x.FechaCreacion).HasColumnType("date");

        builder.Property(x => x.FechaModificacion).HasColumnType("date");
    }
}
