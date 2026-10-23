using Dominio.Entidades;
using Dominio.Interfaces;
using Repositorio.Contexto;

namespace Repositorio.Repositorios;

public class ComentarioRepositorio : BaseRepositorio<Comentario>, IComentarioRepositorio
{
    public ComentarioRepositorio(ProjetoDbContext contexto) : base(contexto)
    {
    }
}