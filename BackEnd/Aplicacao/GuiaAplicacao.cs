using Aplicacao.Interfaces;
using Dominio.Entidades;
using Repositorio.Interfaces;

namespace Aplicacao;

public class GuiaAplicacao : IGuiaAplicacao
{
     readonly IGuiaRepositorio _guiaRespositorio;
    public GuiaAplicacao(IGuiaRepositorio guiaRepositorio)
    {
        _guiaRespositorio = guiaRepositorio ;
    }

    public async Task<Guia> CriarAsync(Guia guia)
    {
        if(guia == null)
        {
            throw new Exception("Guia vazio");
        }
        if(string.IsNullOrEmpty(guia.Titulo))
        {
            throw new Exception("Titulo vazio");
        }

        ValidarInformacoes(guia);
        
        return await _guiaRespositorio.CriarAsync(guia);
    }
    public async void ExcluirAsync(int id)
    {
        var guiaDominio = await _guiaRespositorio.ObterPorIdAsync(id);

        if(guiaDominio == null)
        {
            throw new Exception("Guia não encontrado");
        }

        await _guiaRespositorio.ExcluirAsync(guiaDominio);
    }
    public async void AtualizarAsync(Guia guia)
    {
        var guiaDominio = await _guiaRespositorio.ObterPorIdAsync(guia.GuiaId);

        if(guiaDominio == null)
        {
            throw new Exception("Guia não encontrado");
        }

         if(string.IsNullOrEmpty(guia.Titulo))
        {
            throw new Exception("Titulo vazio");
        }

        guiaDominio.Titulo = guia.Titulo;
        guiaDominio.Conteudo = guia.Conteudo;

        ValidarInformacoes(guia);

        await _guiaRespositorio.AtualizarAsync(guiaDominio);
    }
    public async Task<Guia?> ObterPorIdAsync(int id)
    {
        var guiaDominio = await _guiaRespositorio.ObterPorIdAsync(id);

        if(guiaDominio == null)
        {
            throw new Exception("Guia não encontrado");
        }

        return guiaDominio;
    }
    public async Task<List<Guia>> ObterTodos()
    {
        return await _guiaRespositorio.ObterTodosAsync();
    }
     private static void ValidarInformacoes(Guia guia)
    {
        
        if(string.IsNullOrEmpty(guia.Conteudo))
        {
            throw new Exception("Conteudo vazio");
        }
    }
}