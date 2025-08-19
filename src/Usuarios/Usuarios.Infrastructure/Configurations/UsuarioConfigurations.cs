using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Usuarios.Domain.Shared;
using Usuarios.Domain.Usuarios;

namespace Usuarios.Infrastructure.Configurations;

public class UsuarioConfigurations : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuarios");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.NombrePersona).HasMaxLength(100).IsRequired();
        builder.Property(r => r.ApellidoPaterno).HasMaxLength(100).IsRequired();
        builder.Property(r => r.ApellidoMaterno).HasMaxLength(100);
        builder.Property(p => p!.Password).HasMaxLength(20).HasConversion(
            p => p!.Value,
            value => Password.Create(value).Value);
        builder.Property(p => p!.NombreUsuario).HasMaxLength(20).HasConversion(
            p => p!.Value,
            value => NombreUsuario.Create(value).Value);
        builder.Property(u => u.FechaNacimiento).IsRequired();
        builder.Property(p => p!.CorreoElectronico).HasMaxLength(20).HasConversion(
            p => p!.Value,
            value => CorreoElectronico.Create(value).Value);
        builder.OwnsOne(u => u.Direccion);
        builder.Property(u => u.Estado)
            .HasConversion(
                e => e.ToString(),
                e => (Estados)Enum.Parse(typeof(Estados), e)).IsRequired();
        builder.Property(u => u.FechaUltimoCambio).IsRequired();
        builder.HasOne(u => u.Rol)
            .WithMany()
            .HasForeignKey(u => u.RolId)
            .IsRequired();
        builder.Property<uint>("Version")
            .IsRowVersion()
            .IsConcurrencyToken();
        
        
        

        
    }
}
