using Aplicacao.Interfaces;
using Dominio.Entidades;
using Repositorio.Interfaces;

namespace Aplicacao;

public class JogoAplicacao : IJogoAplicacao
{
     readonly IJogoRepositorio _jogoRepositorio;
    public JogoAplicacao(IJogoRepositorio jogoRepositorio)
    {
        _jogoRepositorio = jogoRepositorio ;
    }

    public async Task<Jogo> CriarAsync(Jogo jogo)
    {
        if(jogo == null)
        {
            throw new Exception("jogo vazio");
        }

        if(string.IsNullOrEmpty(jogo.NomeJogo))
        {
            throw new Exception("Nome vazio");
        }

        return await _jogoRepositorio.CriarAsync(jogo);
    }

    public async void ExcluirAsync(int id)
    {
        var jogoDominio = await _jogoRepositorio.ObterPorIdAsync(id);

        if(jogoDominio == null)
        {
            throw new Exception("Jogo não encontrado");
        }

        await _jogoRepositorio.ExcluirAsync(jogoDominio);
    }

    public async void AtualizarAsync(Jogo jogo)
    {
        var jogoDominio = await _jogoRepositorio.ObterPorIdAsync(jogo.JogoId);

        if(jogoDominio == null)
        {
            throw new Exception("Jogo não encontrado");
        }

       if(string.IsNullOrEmpty(jogo.NomeJogo))
        {
            throw new Exception("Nome vazio");
        }

        jogoDominio.NomeJogo = jogo.NomeJogo;
        jogoDominio.Descricao = jogo.Descricao;


        await _jogoRepositorio.AtualizarAsync(jogoDominio);
    }

    public async Task<Jogo?> ObterPorIdAsync(int id)
    {
        var jogoDominio = await _jogoRepositorio.ObterPorIdAsync(id);

        if(jogoDominio == null)
        {
            throw new Exception("Jogo não encontrado");
        }

        return jogoDominio;
    }

     public async Task<List<Jogo>> ObterTodos()
    {
        return await _jogoRepositorio.ObterTodosAsync();
    }

}