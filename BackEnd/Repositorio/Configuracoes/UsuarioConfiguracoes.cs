using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositorio.Configuracoes;

public class UsuarioConfiguracoes : IEntityTypeConfiguration<Dominio.Entidades.Usuario>
{
    public void Configure(EntityTypeBuilder<Dominio.Entidades.Usuario> builder)
    {
        builder.ToTable("Usuario").HasKey(x => x.UsuarioId);

        builder.Property(nameof(Dominio.Entidades.Usuario.UsuarioId)).HasColumnName("UsuarioId");
        builder.Property(nameof(Dominio.Entidades.Usuario.Nome)).HasColumnName("Nome").IsRequired(true);
        builder.Property(nameof(Dominio.Entidades.Usuario.Email)).HasColumnName("Email").IsRequired(true);
        builder.Property(nameof(Dominio.Entidades.Usuario.Senha)).HasColumnName("Senha").IsRequired(true);

    }
}