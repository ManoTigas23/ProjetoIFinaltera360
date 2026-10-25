namespace Api.DTOs.Auth;

public class TokenRespostaDTO
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiracao { get; set; }
}