using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class GenericoVsSubmodulosConfiguration : IEntityTypeConfiguration<GenericoVsSubmodulos>
{
    public void Configure(EntityTypeBuilder<GenericoVsSubmodulos> builder)
    {
        builder.ToTable("genericovssubmodulos");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);

        builder.Property(x => x.FechaCreacion).HasColumnType("date");

        builder.Property(x => x.FechaModificacion).HasColumnType("date");

        builder.HasOne(x => x.PermisosGenericos).WithMany(x => x.GenericoVsSubmodulos).HasForeignKey(x => x.IdGenericos);
        builder.HasOne(x => x.MaestrosVsSubmodulos).WithMany(x => x.GenericoVsSubmodulos).HasForeignKey(x => x.IdSubmodulos);
        builder.HasOne(x => x.Roles).WithMany(x => x.GenericoVsSubmodulos).HasForeignKey(x => x.IdRol);
    }
}
