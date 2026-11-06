using Dominio.Entidades;

namespace Aplicacao.Interfaces;

public interface IGuiaAplicacao
{
    public Task<Guia> CriarAsync(Guia guia);
    public void ExcluirAsync(int id);
    public void AtualizarAsync(Guia guia);
    public Task<Guia?> ObterPorIdAsync(int id);
    public Task<List<Guia>> ObterTodos();   
}