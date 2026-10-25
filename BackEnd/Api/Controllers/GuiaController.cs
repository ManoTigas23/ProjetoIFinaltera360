using Api.DTOs.Guias;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GuiaController : ControllerBase
{
    private readonly IGuiaRepositorio _guiaRepositorio;

    public GuiaController(IGuiaRepositorio guiaRepositorio)
    {
        _guiaRepositorio = guiaRepositorio;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var guias = await _guiaRepositorio.ObterTodosAsync();

        var guiasDto = guias.Select(guia => new GuiaRespostaDTO
        {
            GuiaId = guia.GuiaId,
            Titulo = guia.Titulo,
            Conteudo = guia.Conteudo,
            JogoId = guia.JogoId,
            VideoYoutube = guia.VideoYoutube,
            UsuarioId = guia.UsuarioId
        });

        return Ok(guiasDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var guia = await _guiaRepositorio.ObterPorIdAsync(id);

        if (guia is null)
            return NotFound("Guia não encontrado.");

        var guiaDto = new GuiaRespostaDTO
        {
            GuiaId = guia.GuiaId,
            Titulo = guia.Titulo,
            Conteudo = guia.Conteudo,
            JogoId = guia.JogoId,
            VideoYoutube = guia.VideoYoutube,
            UsuarioId = guia.UsuarioId
        };

        return Ok(guiaDto);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] GuiaCriacaoDTO dto)
    {
        var guia = new Guia
        {
            Titulo = dto.Titulo,
            Conteudo = dto.Conteudo,
            JogoId = dto.JogoId,
            VideoYoutube = dto.VideoYoutube,
            UsuarioId = dto.UsuarioId
        };

        var novoGuia = await _guiaRepositorio.CriarAsync(guia);

        var respostaDto = new GuiaRespostaDTO
        {
            GuiaId = novoGuia.GuiaId,
            Titulo = novoGuia.Titulo,
            Conteudo = novoGuia.Conteudo,
            JogoId = novoGuia.JogoId,
            VideoYoutube = novoGuia.VideoYoutube,
            UsuarioId = novoGuia.UsuarioId
        };

        return CreatedAtAction(nameof(ObterPorId), new { id = respostaDto.GuiaId }, respostaDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] GuiaCriacaoDTO dto)
    {
        var guiaExistente = await _guiaRepositorio.ObterPorIdAsync(id);
        if (guiaExistente is null)
        {
            return NotFound("Guia não encontrado.");
        }

        guiaExistente.Titulo = dto.Titulo;
        guiaExistente.Conteudo = dto.Conteudo;
        guiaExistente.JogoId = dto.JogoId;
        guiaExistente.VideoYoutube = dto.VideoYoutube;
        guiaExistente.UsuarioId = dto.UsuarioId;

        await _guiaRepositorio.AtualizarAsync(guiaExistente);

        var respostaDto = new GuiaRespostaDTO
        {
            GuiaId = guiaExistente.GuiaId,
            Titulo = guiaExistente.Titulo,
            Conteudo = guiaExistente.Conteudo,
            JogoId = guiaExistente.JogoId,
            VideoYoutube = guiaExistente.VideoYoutube,
            UsuarioId = guiaExistente.UsuarioId
        };

        return Ok(respostaDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var guiaExistente = await _guiaRepositorio.ObterPorIdAsync(id);
        if (guiaExistente is null)
        {
            return NotFound("Guia não encontrado.");
        }

        await _guiaRepositorio.ExcluirAsync(id);
        return Ok("Guia removido com sucesso.");
    }
}