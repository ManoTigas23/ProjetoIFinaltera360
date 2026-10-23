namespace Dominio.Entidades;

public class Comentario
{
    public int comentarioId {get; set;}
    public string texto {get; set;}
    public int guiaId {get; set;}
    public int usuarioId {get; set;}
}