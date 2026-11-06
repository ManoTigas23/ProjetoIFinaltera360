using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositorio.Configuracoes;

public class JogoConfiguracoes : IEntityTypeConfiguration<Dominio.Entidades.Jogo>
{
    public void Configure(EntityTypeBuilder<Dominio.Entidades.Jogo> builder)
    {
        builder.ToTable("Jogo").HasKey(x => x.JogoId);

        builder.Property(nameof(Dominio.Entidades.Jogo.JogoId)).HasColumnName("JogoId");
        builder.Property(nameof(Dominio.Entidades.Jogo.NomeJogo)).HasColumnName("Nome").IsRequired(true);
        builder.Property(nameof(Dominio.Entidades.Jogo.Descricao)).HasColumnName("Descricao").IsRequired(true);
        builder.Property(nameof(Dominio.Entidades.Jogo.CapaJogo)).HasColumnName("Link da Capa").IsRequired(true);
    }
}