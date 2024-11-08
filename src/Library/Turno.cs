namespace Library;

public class Turno
{
    public Jugador JugadorActual { get; private set; }
    public Jugador JugadorOponente { get; private set; }
    public int NumeroTurno { get; private set; }
    private int turnoActual;

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

    // Método para ejecutar un ataque especial con restricción de turnos
    public bool EjecutarAtaqueEspecial(Jugador jugador, Pokemon atacante, Move movimiento, int turnoActual)
    {
        // Verificar si el ataque especial se puede realizar
        if (!jugador.PuedeUsarAtaqueEspecial(movimiento.MoveDetails.Name, turnoActual))
        {
            Console.WriteLine($"No puedes usar el ataque especial {movimiento.MoveDetails.Name} en este momento. Debes esperar más turnos.");
            return false;
        }

        // Realizar el ataque especial
        atacante.Atacar(jugador, JugadorOponente.PokemonActual, movimiento, turnoActual);
        Console.WriteLine($"{jugador.Nombre} usó el ataque especial {movimiento.MoveDetails.Name} causando daño!");

        // Registrar el ataque especial solo si el ataque fue exitoso
        jugador.RegistrarAtaqueEspecial(movimiento.MoveDetails.Name, turnoActual);

        return true;
    }
}
