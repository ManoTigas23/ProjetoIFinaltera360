namespace Dominio.Interfaces;

public interface IBaseRepositorio<T> where T : class
{
    Task<T> CriarAsync(T entidade);
    Task<T?> ObterPorIdAsync(int id);
    Task<List<T>> ObterTodosAsync();
    Task<T> AtualizarAsync(T entidade);
    Task<bool> ExcluirAsync(int id);
}