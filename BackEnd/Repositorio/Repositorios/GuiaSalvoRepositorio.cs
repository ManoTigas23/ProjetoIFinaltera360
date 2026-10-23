using Dominio.Entidades;
using Dominio.Interfaces;
using Repositorio.Contexto;

namespace Repositorio.Repositorios;

public class GuiaSalvoRepositorio : BaseRepositorio<GuiaSalvo>, IGuiaSalvoRepositorio
{
    public GuiaSalvoRepositorio(ProjetoDbContext contexto) : base(contexto)
    {
    }
}