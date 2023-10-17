using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
