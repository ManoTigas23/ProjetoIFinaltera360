using Api.DTOs.Usuarios;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;

    public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
    {
        _usuarioRepositorio = usuarioRepositorio;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var usuarios = await _usuarioRepositorio.ObterTodosAsync();

        var usuariosDto = usuarios.Select(usuario => new UsuarioRespostaDto
        {
            Id = usuario.UsuarioId,
            Nome = usuario.Nome,
            Email = usuario.Email
        });

        return Ok(usuariosDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var usuario = await _usuarioRepositorio.ObterPorIdAsync(id);
        if (usuario == null)
            return NotFound("Usuário não encontrado.");

        var usuarioDto = new UsuarioRespostaDto
        {
            Id = usuario.UsuarioId,
            Nome = usuario.Nome,
            Email = usuario.Email
        };

        return Ok(usuarioDto);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] UsuarioCriacaoDto dto)
    {
        var usuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Senha = dto.Senha
        };

        var novoUsuario = await _usuarioRepositorio.CriarAsync(usuario);

        var respostaDto = new UsuarioRespostaDto
        {
            Id = novoUsuario.UsuarioId,
            Nome = novoUsuario.Nome,
            Email = novoUsuario.Email
        };

        return CreatedAtAction(nameof(ObterPorId), new { id = respostaDto.Id }, respostaDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] UsuarioAtualizacaoDTO dto)
    {
        var usuarioExistente = await _usuarioRepositorio.ObterPorIdAsync(id);
        if (usuarioExistente is null)
        {
            return NotFound("Usuário não encontrado.");
        }

        usuarioExistente.Nome = dto.Nome;
        usuarioExistente.Email = dto.Email;
        if (!string.IsNullOrEmpty(dto.Senha))
        {
            usuarioExistente.Senha = dto.Senha;
        }

        await _usuarioRepositorio.AtualizarAsync(usuarioExistente);

        var respostaDto = new UsuarioRespostaDto
        {
            Id = usuarioExistente.UsuarioId,
            Nome = usuarioExistente.Nome,
            Email = usuarioExistente.Email
        };

        return Ok(respostaDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var usuarioExistente = await _usuarioRepositorio.ObterPorIdAsync(id);
        if (usuarioExistente is null)
        {
            return NotFound("Usuário não encontrado.");
        }

        await _usuarioRepositorio.ExcluirAsync(id);
        return Ok("Usuário removido com sucesso.");
    }
}