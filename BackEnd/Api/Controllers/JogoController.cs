using Api.DTOs.Jogos;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JogoController : ControllerBase
{
    private readonly IJogoRepositorio _jogoRepositorio;

    public JogoController(IJogoRepositorio jogoRepositorio)
    {
        _jogoRepositorio = jogoRepositorio;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var jogos = await _jogoRepositorio.ObterTodosAsync();

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
        var jogo = await _jogoRepositorio.ObterPorIdAsync(id);
        if (jogo == null)
            return NotFound("Jogo não encontrado.");

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
    public async Task<IActionResult> Criar([FromBody] JogoCriacaoDTO dto)
    {
        var jogo = new Jogo
        {
            NomeJogo = dto.NomeJogo,
            Descricao = dto.Descricao,
            CapaJogo = dto.CapaJogo
        };

        var novoJogo = await _jogoRepositorio.CriarAsync(jogo);

        var respostaDto = new JogoRespostaDTO
        {
            JogoId = novoJogo.JogoId,
            NomeJogo = novoJogo.NomeJogo,
            Descricao = novoJogo.Descricao,
            CapaJogo = novoJogo.CapaJogo
        };

        return CreatedAtAction(nameof(ObterPorId), new { id = respostaDto.JogoId }, respostaDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] JogoCriacaoDTO dto)
    {
        var jogoExistente = await _jogoRepositorio.ObterPorIdAsync(id);
        if (jogoExistente is null)
        {
            return NotFound("Jogo não encontrado.");
        }

        jogoExistente.NomeJogo = dto.NomeJogo;
        jogoExistente.Descricao = dto.Descricao;
        jogoExistente.CapaJogo = dto.CapaJogo;

        await _jogoRepositorio.AtualizarAsync(jogoExistente);

        var respostaDto = new JogoRespostaDTO
        {
            JogoId = jogoExistente.JogoId,
            NomeJogo = jogoExistente.NomeJogo,
            Descricao = jogoExistente.Descricao,
            CapaJogo = jogoExistente.CapaJogo
        };

        return Ok(respostaDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var jogoExistente = await _jogoRepositorio.ObterPorIdAsync(id);
        if (jogoExistente is null)
        {
            return NotFound("Jogo não encontrado.");
        }

        await _jogoRepositorio.ExcluirAsync(id);
        return Ok("Jogo removido com sucesso.");
    }
}