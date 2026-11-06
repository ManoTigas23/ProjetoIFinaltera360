using Api.DTOs.Usuarios;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Aplicacao.Interfaces;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioAplicacao _usuarioAplicacao;

    public UsuarioController(IUsuarioAplicacao usuarioAplicacao)
    {
        _usuarioAplicacao = usuarioAplicacao;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var usuarios = await _usuarioAplicacao.ObterTodos();

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
        var usuario = await _usuarioAplicacao.ObterPorIdAsync(id);
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
    public IActionResult Criar([FromBody] UsuarioCriacaoDto dto)
    {
        try
        {
            var usuarioDominio = new Usuario()
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha
            };

            var usuarioID = _usuarioAplicacao.CriarAsync(usuarioDominio);

            return Ok(usuarioID);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public IActionResult Atualizar([FromBody] UsuarioAtualizacaoDTO dto)
    {
        try
        {
            var usuarioDominio = new Usuario()
            {
                UsuarioId = dto.Id,
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha
            };

            _usuarioAplicacao.AtualizarAsync(usuarioDominio);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("Deletar/{usuarioId}")]
    public IActionResult Deletar([FromRoute] int usuarioId)
    {
        try
        {
            _usuarioAplicacao.ExcluirAsync(usuarioId);

            return Ok("Usuário removido com sucesso.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}