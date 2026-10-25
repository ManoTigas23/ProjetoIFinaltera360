namespace Api.DTOs.Guias;

public class GuiaRespostaDTO
{
    public int GuiaId {get; set;}
    public string Titulo { get; set; } = string.Empty;
    public string Conteudo { get; set; } = string.Empty;
    public int JogoId { get; set; } 
    public string VideoYoutube { get; set; } = string.Empty;
    public int UsuarioId {get; set;}
}