namespace Library;

public class Turno
{
    public Jugador JugadorActual { get; private set; }
    public Jugador JugadorOponente { get; private set; }
    public int NumeroTurno { get; private set; }

    public Turno(Jugador jugador1, Jugador jugador2)
    {
        JugadorActual = jugador1;
        JugadorOponente = jugador2;
        NumeroTurno = 1;
    }

    public void CambiarTurno()
    {
        var temp = JugadorActual;
        JugadorActual = JugadorOponente;
        JugadorOponente = temp;
        NumeroTurno++;
    }

    public void MostrarTurno()
    {
        Console.WriteLine($"-- Turno {NumeroTurno} / {JugadorActual.Nombre} es tu turno! --");
    }
}
