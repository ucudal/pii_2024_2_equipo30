namespace Library;

public class Movimiento
{
    public string Nombre { get; set; }
    public int Poder { get; set; }
    public EstadoEspecial EstadoEspecial { get; set; }

    public Movimiento(string nombre, int poder, EstadoEspecial estadoEspecial = EstadoEspecial.Ninguno)
    {
        Nombre = nombre;
        Poder = poder;
        EstadoEspecial = estadoEspecial;
    }
}


