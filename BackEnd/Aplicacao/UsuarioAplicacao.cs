using Aplicacao.Interfaces;
using Dominio.Entidades;
using Repositorio.Interfaces;

namespace Aplicacao;
public class UsuarioAplicacao : IUsuarioAplicacao
{
    readonly IUsuarioRepositorio _usuarioRespositorio;
    public UsuarioAplicacao(IUsuarioRepositorio usuarioRepositorio)
    {
        _usuarioRespositorio = usuarioRepositorio ;
    }

    public async Task<Usuario> CriarAsync(Usuario usuario)
    {
        if(usuario == null)
        {
            throw new Exception("Usuário vazio");
        }
        if(string.IsNullOrEmpty(usuario.Senha))
        {
            throw new Exception("Senha vazia");
        }

        ValidarInformacoes(usuario);
        

        return await _usuarioRespositorio.CriarAsync(usuario);
    }
    public async void ExcluirAsync(int id)
    {
        var usuarioDominio = await _usuarioRespositorio.ObterPorIdAsync(id);

        if(usuarioDominio == null)
        {
            throw new Exception("Usuário não encontrado");
        }

        await _usuarioRespositorio.ExcluirAsync(usuarioDominio);
    }
    public async void AtualizarAsync(Usuario usuario)
    {
        var usuarioDominio = await _usuarioRespositorio.ObterPorIdAsync(usuario.UsuarioId);

        if(usuarioDominio == null)
        {
            throw new Exception("Usuário não encontrado");
        }

        if(string.IsNullOrEmpty(usuario.Senha))
        {
            throw new Exception("Senha vazia");
        }

        usuarioDominio.Nome = usuario.Nome;
        usuarioDominio.Email = usuario.Email;

        ValidarInformacoes(usuario);

        await _usuarioRespositorio.AtualizarAsync(usuarioDominio);
    }
    public async Task<Usuario?> ObterPorIdAsync(int id)
    {
        var usuarioDominio = await _usuarioRespositorio.ObterPorIdAsync(id);

        if(usuarioDominio == null)
        {
            throw new Exception("Usuário não encontrado");
        }

        return usuarioDominio;
    }
    public async Task<List<Usuario>> ObterTodos()
    {
        return await _usuarioRespositorio.ObterTodosAsync();
    }
    private static void ValidarInformacoes(Usuario usuario)
    {
        
        if(string.IsNullOrEmpty(usuario.Email))
        {
            throw new Exception("Email vazio");
        }
        if(string.IsNullOrEmpty(usuario.Nome))
        {
            throw new Exception("Nome vazio");
        }
    }

}