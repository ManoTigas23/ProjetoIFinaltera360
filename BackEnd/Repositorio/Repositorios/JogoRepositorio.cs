using Dominio.Entidades;
using Repositorio.Interfaces;
using Repositorio.Contexto;

namespace Repositorio.Repositorios;

public class JogoRepositorio : BaseRepositorio<Jogo>, IJogoRepositorio
{
    public JogoRepositorio(ProjetoDbContext contexto) : base(contexto)
    {
    }
}