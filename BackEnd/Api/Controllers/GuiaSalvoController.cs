using Api.DTOs.GuiasSalvos;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GuiaSalvoController : ControllerBase
{
    private readonly IGuiaSalvoRepositorio _guiaSalvoRepositorio;

    public GuiaSalvoController(IGuiaSalvoRepositorio guiaSalvoRepositorio)
    {
        _guiaSalvoRepositorio = guiaSalvoRepositorio;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var guiasSalvos = await _guiaSalvoRepositorio.ObterTodosAsync();

        var guiasSalvosDto = guiasSalvos.Select(gs => new GuiaSalvoRespostaDTO
        {
            GuiaSalvoId = gs.GuiaSalvoId, 
            UsuarioId = gs.UsuarioId,
            GuiaId = gs.GuiaId
        });

        return Ok(guiasSalvosDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var guiaSalvo = await _guiaSalvoRepositorio.ObterPorIdAsync(id);

        if (guiaSalvo is null)
            return NotFound("Guia Salvo não encontrado.");

        var guiaSalvoDto = new GuiaSalvoRespostaDTO
        {
            GuiaSalvoId = guiaSalvo.GuiaSalvoId, 
            UsuarioId = guiaSalvo.UsuarioId,
            GuiaId = guiaSalvo.GuiaId
        };

        return Ok(guiaSalvoDto);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] GuiaSalvoCriacaoDTO dto)
    {
        var guiaSalvo = new GuiaSalvo
        {
            UsuarioId = dto.UsuarioId,
            GuiaId = dto.GuiaId
        };

        var novoGuiaSalvo = await _guiaSalvoRepositorio.CriarAsync(guiaSalvo);

        var respostaDto = new GuiaSalvoRespostaDTO
        {
            GuiaSalvoId = novoGuiaSalvo.GuiaSalvoId,
            UsuarioId = novoGuiaSalvo.UsuarioId,
            GuiaId = novoGuiaSalvo.GuiaId
        };

        return CreatedAtAction(nameof(ObterPorId), new { id = respostaDto.GuiaSalvoId }, respostaDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] GuiaSalvoCriacaoDTO dto)
    {
        var guiaSalvoExistente = await _guiaSalvoRepositorio.ObterPorIdAsync(id);
        if (guiaSalvoExistente is null)
        {
            return NotFound("Guia Salvo não encontrado.");
        }

        guiaSalvoExistente.UsuarioId = dto.UsuarioId;
        guiaSalvoExistente.GuiaId = dto.GuiaId;

        await _guiaSalvoRepositorio.AtualizarAsync(guiaSalvoExistente);

        var respostaDto = new GuiaSalvoRespostaDTO
        {
            GuiaSalvoId = guiaSalvoExistente.GuiaSalvoId,
            UsuarioId = guiaSalvoExistente.UsuarioId,
            GuiaId = guiaSalvoExistente.GuiaId
        };

        return Ok(respostaDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var guiaSalvoExistente = await _guiaSalvoRepositorio.ObterPorIdAsync(id);
        if (guiaSalvoExistente is null)
        {
            return NotFound("Guia Salvo não encontrado.");
        }

        await _guiaSalvoRepositorio.ExcluirAsync(id);
        return Ok("Guia salvo removido com sucesso.");
    }
}