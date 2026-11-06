using Dominio.Entidades;

namespace Aplicacao.Interfaces;

public interface IJogoAplicacao
{
    public Task<Jogo> CriarAsync(Jogo jogo);
    public void ExcluirAsync(int id);
    public void AtualizarAsync(Jogo jogo);
    public Task<Jogo?> ObterPorIdAsync(int id);
    public Task<List<Jogo>> ObterTodos();   
}