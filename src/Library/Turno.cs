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
    public bool EjecutarAtaqueEspecial(Jugador jugador, Pokemon pokemon, Move movimiento, int turnoActual)
    {
        // Verificar si el ataque especial está disponible según el turno actual
        if (jugador.PuedeUsarAtaqueEspecial(movimiento.MoveDetails.Name, turnoActual))
        {
            // Ejecuta el ataque especial y aplica daño
            pokemon.Atacar(jugador.PokemonActual, JugadorOponente.PokemonActual, movimiento);
            Console.WriteLine($"{jugador.Nombre} usa el ataque especial {movimiento.MoveDetails.Name} causando daño!");
        
            // Registrar el turno actual en el que se usó el ataque especial
            jugador.RegistrarAtaqueEspecial(movimiento.MoveDetails.Name, turnoActual);

            return true; // Ataque especial realizado exitosamente
        }
        else
        {
            // El ataque especial no se puede usar debido a la restricción de turnos
            Console.WriteLine($"{jugador.Nombre} no puede usar el ataque especial {movimiento.MoveDetails.Name} nuevamente. Debe esperar.");
            return false; // Ataque especial bloqueado
        }
    }
}
