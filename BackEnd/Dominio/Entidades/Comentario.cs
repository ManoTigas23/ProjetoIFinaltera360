namespace Dominio.Entidades;

public class Comentario
{
    public int ComentarioId {get; set;}
    public string Texto {get; set;}
    public int GuiaId {get; set;}
    public int UsuarioId {get; set;}
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}