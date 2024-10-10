namespace Library;

public class Tipo
{
    public string Nombre { get; set; }
    public Dictionary<string, double> Efectividad { get; set; } // Tipo -> Modificador de daño

    public Tipo(string nombre)
    {
        Nombre = nombre;
        Efectividad = new Dictionary<string, double>();
    }

    public void AgregarEfectividad(string tipoOponente, double modificador)
    {
        Efectividad[tipoOponente] = modificador;
    }
}