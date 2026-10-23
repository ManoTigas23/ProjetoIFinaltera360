using Dominio.Entidades;
using Dominio.Interfaces;
using Repositorio.Contexto;

namespace Repositorio.Repositorios;

public class GuiaRepositorio : BaseRepositorio<Guia>, IGuiaRepositorio
{
    public GuiaRepositorio(ProjetoDbContext contexto) : base(contexto)
    {
    }
}