using Dominio.Entidades;

namespace Aplicacao.Interfaces;

public interface IUsuarioAplicacao
{
    public Task<Usuario> CriarAsync(Usuario usuario);
    public void ExcluirAsync(int id);
    public void AtualizarAsync(Usuario usuario);
    public Task<Usuario?> ObterPorIdAsync(int id);
    public Task<List<Usuario>> ObterTodos();
}