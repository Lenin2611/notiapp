using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class MaestrosVsSubmodulosConfiguration : IEntityTypeConfiguration<MaestrosVsSubmodulos>
{
    public void Configure(EntityTypeBuilder<MaestrosVsSubmodulos> builder)
    {
        builder.ToTable("maestrosvssubmodulos");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);

        builder.Property(x => x.FechaCreacion).HasColumnType("date");

        builder.Property(x => x.FechaModificacion).HasColumnType("date");

        builder.HasOne(x => x.ModuloMaestros).WithMany(x => x.MaestrosVsSubmodulos).HasForeignKey(x => x.IdMaestro);
        builder.HasOne(x => x.Submodulos).WithMany(x => x.MaestrosVsSubmodulos).HasForeignKey(x => x.IdSubmodulos);
    }
}
