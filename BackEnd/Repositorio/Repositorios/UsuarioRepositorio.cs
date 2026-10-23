using Dominio.Entidades;
using Dominio.Interfaces;
using Repositorio.Contexto;

namespace Repositorio.Repositorios;

public class UsuarioRepositorio : BaseRepositorio<Usuario>, IUsuarioRepositorio
{
    public UsuarioRepositorio(ProjetoDbContext contexto) : base(contexto)
    {
    }
}