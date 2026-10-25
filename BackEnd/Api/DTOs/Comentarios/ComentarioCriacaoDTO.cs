namespace Api.DTOs.Comentarios;

public class ComentarioCriacaoDTO
{
    public string Texto { get; set; } = string.Empty;
    public int GuiaId { get; set; } 
    public int UsuarioId { get; set; } 
}