using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/controller")]
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
        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var usuario = await _usuarioRepositorio.ObterPorIdAsync(id);
        
        if (usuario is null)
            return NotFound("Usuário não encontrado.");

        return Ok(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] Usuario usuario)
    {
        var novoUsuario = await _usuarioRepositorio.CriarAsync(usuario);
        return CreatedAtAction(nameof(ObterPorId), new { id = novoUsuario.usuarioId }, novoUsuario);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] Usuario usuario)
    {
        if (id != usuario.usuarioId)
        {
            return BadRequest("O ID não está correto");
        }

        var usuarioExistente = await _usuarioRepositorio.ObterPorIdAsync(id);
        if (usuarioExistente is null)
            {
                return NotFound("Usuário não encontrado.");
            }
        var usuarioAtualizacao = await _usuarioRepositorio.AtualizarAsync(usuario);
        return Ok(usuarioAtualizacao);    
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var usuarioExistente = await _usuarioRepositorio.ObterPorIdAsync(id);
        if (usuarioExistente is null)
            {
                return NotFound("Usuário não encontrado.");
            }
        var usuarioDeletado = await _usuarioRepositorio.ExcluirAsync(id);
        return Ok(usuarioDeletado);
    }
}