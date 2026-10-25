namespace Api.DTOs.Comentarios;

public class ComentarioRespostaDTO
{
    public int ComentarioId {get; set;}
    public string Texto { get; set; } = string.Empty;
    public int GuiaId { get; set; } 
    public int UsuarioId { get; set; }
    public DateTime DataCriacao { get; set; }
}