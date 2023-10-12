using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class ModuloMaestrosConfiguration : IEntityTypeConfiguration<ModuloMaestros>
{
    public void Configure(EntityTypeBuilder<ModuloMaestros> builder)
    {
        builder.ToTable("moduulomaestros");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);

        builder.Property(x => x.NombreModulo).IsRequired().HasMaxLength(100);

        builder.Property(x => x.FechaCreacion).HasColumnType("date");

        builder.Property(x => x.FechaModificacion).HasColumnType("date");
    }
}
