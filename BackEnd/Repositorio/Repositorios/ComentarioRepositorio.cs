using Dominio.Entidades;
using Repositorio.Interfaces;
using Repositorio.Contexto;

namespace Repositorio.Repositorios;

public class ComentarioRepositorio : BaseRepositorio<Comentario>, IComentarioRepositorio
{
    public ComentarioRepositorio(ProjetoDbContext contexto) : base(contexto)
    {
    }
}