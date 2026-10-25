namespace Api.DTOs.Jogos;

public class JogoRespostaDTO
{
    public string NomeJogo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string CapaJogo { get; set; } = string.Empty;
    public int JogoId { get; set; }
}