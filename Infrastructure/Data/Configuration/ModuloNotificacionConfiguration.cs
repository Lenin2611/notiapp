using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class ModuloNotificacionConfiguration : IEntityTypeConfiguration<ModuloNotificacion>
{
    public void Configure(EntityTypeBuilder<ModuloNotificacion> builder)
    {
        builder.ToTable("modulonotificacion");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);

        builder.Property(x => x.AsuntoNotificacion).IsRequired().HasMaxLength(80);

        builder.Property(x => x.TextoNotificacion).IsRequired().HasMaxLength(2000);

        builder.Property(x => x.FechaCreacion).HasColumnType("date");

        builder.Property(x => x.FechaModificacion).HasColumnType("date");

        builder.Property(x => x.IdTipoNotificacion).HasColumnType("int");
        builder.HasOne(x => x.TipoNotificaciones).WithMany(x => x.ModuloNotificaciones).HasForeignKey(x => x.IdTipoNotificacion);
        
        builder.Property(x => x.IdRadicado).HasColumnType("int");
        builder.HasOne(x => x.Radicados).WithMany(x => x.ModuloNotificaciones).HasForeignKey(x => x.IdRadicado);
        
        builder.Property(x => x.IdEstadoNotificacion).HasColumnType("int");
        builder.HasOne(x => x.EstadoNotificaciones).WithMany(x => x.ModuloNotificaciones).HasForeignKey(x => x.IdEstadoNotificacion);
        
        builder.Property(x => x.IdHiloRespuesta).HasColumnType("int");
        builder.HasOne(x => x.HiloRespuestaNotificaciones).WithMany(x => x.ModuloNotificaciones).HasForeignKey(x => x.IdHiloRespuesta);
        
        builder.Property(x => x.IdFormato).HasColumnType("int");
        builder.HasOne(x => x.Formatos).WithMany(x => x.ModuloNotificaciones).HasForeignKey(x => x.IdFormato);
        
        builder.Property(x => x.IdRequerimiento).HasColumnType("int");
        builder.HasOne(x => x.TipoRequerimientos).WithMany(x => x.ModuloNotificaciones).HasForeignKey(x => x.IdRequerimiento);
    }
}
