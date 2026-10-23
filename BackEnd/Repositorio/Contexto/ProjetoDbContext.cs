using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Repositorio.Contexto;
public class ProjetoDbContext : DbContext
{
   public ProjetoDbContext(DbContextOptions<ProjetoDbContext> options) : base(options) { }
    public DbSet<Guia> Guias {get; set;}
    public DbSet<Usuario> Usuarios {get; set;}
    public DbSet<Jogo> Jogos {get; set;}
    public DbSet<Comentario> Comentarios {get; set;}
    public DbSet<GuiaSalvo> GuiasSalvos {get; set;}
}