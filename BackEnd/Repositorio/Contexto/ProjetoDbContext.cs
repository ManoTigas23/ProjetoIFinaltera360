using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Repositorio.Configuracoes;

namespace Repositorio.Contexto;

public class ProjetoDbContext : DbContext
{
    public DbSet<Usuario> Usuarios {get; set;}
    public DbSet<Jogo> jogos {get; set;}
    public DbSet<Guia> guias {get; set;}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Filename=./ProjetoFinalItera360.sqlite");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioConfiguracoes());
        modelBuilder.ApplyConfiguration(new JogoConfiguracoes());
        modelBuilder.ApplyConfiguration(new GuiaConfiguracoes());
    }
}