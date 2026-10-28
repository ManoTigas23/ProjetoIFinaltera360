using Microsoft.EntityFrameworkCore;
using Repositorio.Contexto;
using Dominio.Interfaces;

namespace Repositorio;

public  class BaseRepositorio<T> : IBaseRepositorio<T> where T : class
{
    protected readonly ProjetoDbContext _contexto;

    protected BaseRepositorio(ProjetoDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<T> CriarAsync(T entidade)
    {
        _contexto.Set<T>().Add(entidade);
        await _contexto.SaveChangesAsync();
        return entidade;
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        var entidadeExistente = await _contexto.Set<T>().FindAsync(id);
        if (entidadeExistente is null)
        {
            return false;
        }

        _contexto.Set<T>().Remove(entidadeExistente);
        await _contexto.SaveChangesAsync();
        return true;
    }

    public async Task<T?> ObterPorIdAsync(int id)
    {
        var resultado = await _contexto.Set<T>().FindAsync(id);
        return resultado;
    }

    public async Task<List<T>> ObterTodosAsync()
    {
        var resultado = await _contexto.Set<T>().ToListAsync();
        return resultado;
    }
    public async Task<T> AtualizarAsync(T entidade)
    {
        _contexto.Set<T>().Update(entidade);
        await _contexto.SaveChangesAsync();
        return entidade;
    }
}