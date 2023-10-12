using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class TipoNotificacionConfiguration : IEntityTypeConfiguration<TipoNotificaciones>
{
    public void Configure(EntityTypeBuilder<TipoNotificaciones> builder)
    {
        builder.ToTable("tiponotificaciones");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);
        
        builder.Property(x => x.NombreTipo).IsRequired().HasMaxLength(80);

        builder.Property(x => x.FechaCreacion).HasColumnType("date");

        builder.Property(x => x.FechaModificacion).HasColumnType("date");
    }
}
