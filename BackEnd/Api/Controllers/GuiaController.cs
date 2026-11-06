using Api.DTOs.Guias;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Aplicacao.Interfaces; 

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GuiaController : ControllerBase
{
    private readonly IGuiaAplicacao _guiaAplicacao;

    public GuiaController(IGuiaAplicacao guiaAplicacao)
    {
        _guiaAplicacao = guiaAplicacao;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var guias = await _guiaAplicacao.ObterTodos();

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
        var guia = await _guiaAplicacao.ObterPorIdAsync(id);
        if (guia == null)
        {
            return NotFound("Guia não encontrado.");
        }

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
    public IActionResult Criar([FromBody] GuiaCriacaoDTO dto)
    {
        try
        {
            var guiaDominio = new Guia()
            {
                Titulo = dto.Titulo,
                Conteudo = dto.Conteudo,
                JogoId = dto.JogoId,
                VideoYoutube = dto.VideoYoutube,
                UsuarioId = dto.UsuarioId
            };

            var guiaId = _guiaAplicacao.CriarAsync(guiaDominio);

            return Ok(guiaId);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("Deletar/{guiaId}")]
    public IActionResult Deletar([FromRoute] int guiaId)
    {
        try
        {
            _guiaAplicacao.ExcluirAsync(guiaId);
            return Ok("Guia removido com sucesso.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}