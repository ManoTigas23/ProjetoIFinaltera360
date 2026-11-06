using Api.DTOs.Jogos;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Aplicacao.Interfaces;
namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JogoController : ControllerBase
{
    private readonly IJogoAplicacao _jogoAplicacao;

    public JogoController(IJogoAplicacao jogoAplicacao)
    {
        _jogoAplicacao = jogoAplicacao;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var jogos = await _jogoAplicacao.ObterTodos();

        var jogosDto = jogos.Select(jogo => new JogoRespostaDTO
        {
            JogoId = jogo.JogoId,
            NomeJogo = jogo.NomeJogo,
            Descricao = jogo.Descricao,
            CapaJogo = jogo.CapaJogo
        });

        return Ok(jogosDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var jogo = await _jogoAplicacao.ObterPorIdAsync(id);
        if (jogo == null)
        {
            return NotFound("Jogo não encontrado.");
        }

        var jogoDto = new JogoRespostaDTO
        {
            JogoId = jogo.JogoId,
            NomeJogo = jogo.NomeJogo,
            Descricao = jogo.Descricao,
            CapaJogo = jogo.CapaJogo
        };

        return Ok(jogoDto);
    }

    [HttpPost]
    public IActionResult Criar([FromBody] JogoCriacaoDTO dto)
    {
        try
        {
            var jogoDominio = new Jogo()
            {
                NomeJogo = dto.NomeJogo,
                Descricao = dto.Descricao,
                CapaJogo = dto.CapaJogo
            };

            var jogoID = _jogoAplicacao.CriarAsync(jogoDominio);

            return Ok(jogoID);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("Deletar/{jogoId}")]
    public IActionResult Deletar([FromRoute] int jogoId)
    {
        try
        {
            _jogoAplicacao.ExcluirAsync(jogoId);
            return Ok("Jogo removido com sucesso.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}