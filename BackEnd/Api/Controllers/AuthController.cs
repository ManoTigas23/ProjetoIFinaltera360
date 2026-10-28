using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.DTOs.Auth;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly IConfiguration _configuration;

    public AuthController(IUsuarioRepositorio usuarioRepositorio, IConfiguration configuration)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _configuration = configuration;
    }

    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar([FromBody] RegistroDTO dto)
    {
        var usuarios = await _usuarioRepositorio.ObterTodosAsync();
        if (usuarios.Any(u => u.Email == dto.Email))
        {
            return BadRequest("Este e-mail já está em uso.");
        }

        var novoUsuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha)
        };

        await _usuarioRepositorio.CriarAsync(novoUsuario);

        return Ok(new { mensagem = "Utilizador registado com sucesso!" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        var usuarios = await _usuarioRepositorio.ObterTodosAsync();
        var usuario = usuarios.FirstOrDefault(u => u.Email == dto.Email);

        if (usuario is null)
            return Unauthorized("Credenciais inválidas.");

        bool senhaValida = BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.Senha);
        if (!senhaValida)
            return Unauthorized("Credenciais inválidas.");

        var tokenResposta = GerarTokenJwt(usuario);

        return Ok(tokenResposta);
    }

    private TokenRespostaDTO GerarTokenJwt(Usuario usuario)
    {
        var chaveSecreta = _configuration["Jwt:JogosIndiesSaoGeralmenteMelhores"]!;
        var emissor = _configuration["Jwt:Emissor"];
        var audiencia = _configuration["Jwt:Audiencia"];
        var expiracaoHoras = int.Parse(_configuration["Jwt:ExpiracaoEmHoras"] ?? "8");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Email, usuario.Email)
        };

        var expiracao = DateTime.UtcNow.AddHours(expiracaoHoras);

        var token = new JwtSecurityToken(
            issuer: emissor,
            audience: audiencia,
            claims: claims,
            expires: expiracao,
            signingCredentials: creds
        );

        return new TokenRespostaDTO
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiracao = expiracao
        };
    }
}