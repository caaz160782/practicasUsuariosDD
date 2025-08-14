using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Usuarios.Domain.Roles;

namespace Usuarios.Infrastructure.Configurations;

public class RolConfigurations : IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> builder)
    {
        builder.ToTable("roles");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Descripcion).HasMaxLength(250);

        builder.HasIndex(r => r.Nombre).IsUnique();
    }
}