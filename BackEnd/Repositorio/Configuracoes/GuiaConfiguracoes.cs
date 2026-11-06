using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositorio.Configuracoes;

public class GuiaConfiguracoes : IEntityTypeConfiguration<Dominio.Entidades.Guia>
{
    public void Configure(EntityTypeBuilder<Dominio.Entidades.Guia> builder)
    {
        builder.ToTable("Guia").HasKey(x => x.GuiaId);

        builder.Property(nameof(Dominio.Entidades.Guia.GuiaId)).HasColumnName("GuiaID");
        builder.Property(nameof(Dominio.Entidades.Guia.Conteudo)).HasColumnName("Conteudo").IsRequired(true);
        builder.Property(nameof(Dominio.Entidades.Guia.UsuarioId)).HasColumnName("UsuarioID").IsRequired(true);
        builder.Property(nameof(Dominio.Entidades.Guia.JogoId)).HasColumnName("JogoID").IsRequired(true);
        builder.Property(nameof(Dominio.Entidades.Guia.VideoYoutube)).HasColumnName("LinkVideo");
        builder.Property(nameof(Dominio.Entidades.Guia.Titulo)).HasColumnName("TituloGuia").IsRequired(true);

    }
}