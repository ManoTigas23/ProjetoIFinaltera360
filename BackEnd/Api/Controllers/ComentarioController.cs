using Api.DTOs.Comentarios;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComentarioController : ControllerBase
{
    private readonly IComentarioRepositorio _comentarioRepositorio;

    public ComentarioController(IComentarioRepositorio comentarioRepositorio)
    {
        _comentarioRepositorio = comentarioRepositorio;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var comentarios = await _comentarioRepositorio.ObterTodosAsync();

        var comentariosDto = comentarios.Select(comentario => new ComentarioRespostaDTO
        {
            ComentarioId = comentario.ComentarioId, 
            Texto = comentario.Texto,
            GuiaId = comentario.GuiaId,
            UsuarioId = comentario.UsuarioId,
            DataCriacao = comentario.DataCriacao
        });

        return Ok(comentariosDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var comentario = await _comentarioRepositorio.ObterPorIdAsync(id);

        if (comentario is null)
            return NotFound("Comentário não encontrado.");

        var comentarioDto = new ComentarioRespostaDTO
        {
            ComentarioId = comentario.ComentarioId, 
            Texto = comentario.Texto,
            GuiaId = comentario.GuiaId,
            UsuarioId = comentario.UsuarioId,
            DataCriacao = comentario.DataCriacao
        };

        return Ok(comentarioDto);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] ComentarioCriacaoDTO dto)
    {
        var comentario = new Comentario
        {
            Texto = dto.Texto,
            GuiaId = dto.GuiaId,
            UsuarioId = dto.UsuarioId,
            DataCriacao = DateTime.UtcNow
        };

        var novoComentario = await _comentarioRepositorio.CriarAsync(comentario);

        var respostaDto = new ComentarioRespostaDTO
        {
            ComentarioId = novoComentario.ComentarioId,
            Texto = novoComentario.Texto,
            GuiaId = novoComentario.GuiaId,
            UsuarioId = novoComentario.UsuarioId,
            DataCriacao = novoComentario.DataCriacao
        };

        return CreatedAtAction(nameof(ObterPorId), new { id = respostaDto.ComentarioId }, respostaDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] ComentarioCriacaoDTO dto)
    {
        var comentarioExistente = await _comentarioRepositorio.ObterPorIdAsync(id);
        if (comentarioExistente is null)
        {
            return NotFound("Comentário não encontrado.");
        }

        comentarioExistente.Texto = dto.Texto;
        comentarioExistente.GuiaId = dto.GuiaId;
        comentarioExistente.UsuarioId = dto.UsuarioId;

        await _comentarioRepositorio.AtualizarAsync(comentarioExistente);

        var respostaDto = new ComentarioRespostaDTO
        {
            ComentarioId = comentarioExistente.ComentarioId,
            Texto = comentarioExistente.Texto,
            GuiaId = comentarioExistente.GuiaId,
            UsuarioId = comentarioExistente.UsuarioId,
            DataCriacao = comentarioExistente.DataCriacao
        };

        return Ok(respostaDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var comentarioExistente = await _comentarioRepositorio.ObterPorIdAsync(id);
        if (comentarioExistente is null)
        {
            return NotFound("Comentário não encontrado.");
        }

        await _comentarioRepositorio.ExcluirAsync(id);
        return Ok("Comentário removido com sucesso.");
    }
}