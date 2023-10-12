using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class RolVsMaestroConfiguration : IEntityTypeConfiguration<RolVsMaestro>
{
    public void Configure(EntityTypeBuilder<RolVsMaestro> builder)
    {
        builder.ToTable("rolvsmaestro");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);

        builder.Property(x => x.FechaCreacion).HasColumnType("date");

        builder.Property(x => x.FechaModificacion).HasColumnType("date");

        builder.HasOne(x => x.Roles).WithMany(x => x.RolVsMaestros).HasForeignKey(x => x.IdRol);
        builder.HasOne(x => x.ModuloMaestros).WithMany(x => x.RolVsMaestros).HasForeignKey(x => x.IdMaestro);
    }
}
