using System.Threading.Channels;

namespace Library;

public class Batalla
{
    private Jugador jugador1;
    private Jugador jugador2;
    private Turno turno;

    public Batalla(Jugador jugador1, Jugador jugador2)
    {
        this.jugador1 = jugador1;
        this.jugador2 = jugador2;
        this.turno = new Turno(jugador1, jugador2);
    }

    public void IniciarBatalla()
    {
        Console.WriteLine($"Equipo de {jugador1.Nombre}: ");
        // Mostrar Pokémon del jugador
        for (int i = 0; i < jugador1.Equipo.Count; i++)
        {
            var pokemon = jugador1.Equipo[i]; 
            if (!pokemon.EstaFueraDeCombate())
            {
                Console.WriteLine($"{i + 1}: {pokemon.Name} - {pokemon.Health} de vida");
            }
        }
        Console.WriteLine($"Equipo de {jugador2.Nombre}: ");
        // Mostrar Pokémon del jugador
        for (int i = 0; i < jugador2.Equipo.Count; i++)
        {
            var pokemon = jugador2.Equipo[i]; 
            if (!pokemon.EstaFueraDeCombate())
            {
                Console.WriteLine($"{i + 1}: {pokemon.Name} - {pokemon.Health} de vida");
            }
        }
        while (!jugador1.TodosFueraDeCombate() && !jugador2.TodosFueraDeCombate())
        {
            turno.MostrarTurno();
            JugarTurno(turno.JugadorActual, turno.JugadorOponente);
            turno.CambiarTurno();
        }

        // Mostrar quién ganó
        if (jugador1.TodosFueraDeCombate())
        {
            Console.WriteLine($"{jugador2.Nombre} gana la batalla!");
        }
        else
        {
            Console.WriteLine($"{jugador1.Nombre} gana la batalla!");
        }
    }

    private void JugarTurno(Jugador jugadorActual, Jugador jugadorOponente)
    {
        Console.WriteLine($"{jugadorActual.Nombre} elige la acción que deseas hacer: ");
        Console.WriteLine($"1: Atacar al pokemon contrario");
        Console.WriteLine($"2: Cambiar el pokemon actual por otro");
        string Election = Console.ReadLine();
        if (Election == "1")
        {
            Pokemon PokemonActual = jugadorActual.PokemonActual;
            if (PokemonActual.Moves == null || PokemonActual.Moves.Count == 0)
            {
                Console.WriteLine($"{PokemonActual.Name} no tiene movimientos disponibles.");
                return;
            }
        
            if (!PokemonActual.PuedeAtacar())
            {
                Console.WriteLine($"{PokemonActual.Name} no puede atacar este turno.");
                return;
            }
            // Mostrar movimientos del Pokémon seleccionado
            Console.WriteLine($"{jugadorActual.Nombre} elige un movimiento de: {PokemonActual.Name}");

            for (int i = 0; i < PokemonActual.Moves.Count; i++)
            {
                var movimiento = PokemonActual.Moves[i];
                Console.WriteLine($"{i + 1}: {movimiento.MoveDetails.Name} (Poder: {movimiento.MoveDetails.Power}) (Precisión: {movimiento.MoveDetails.Accuracy}) Especial: {movimiento.EstadoEspecial}");
            }

            // Elección del movimiento
            int movimientoSeleccionado = int.Parse(Console.ReadLine()) - 1;

            // Realizar el ataque
            PokemonActual.Atacar(jugadorActual.PokemonActual,jugadorOponente.PokemonActual, PokemonActual.Moves[movimientoSeleccionado]); // Suponiendo que el oponente es el primer Pokémon
        }
        else if (Election == "2")
        {
            Console.WriteLine($"Pokemon Actual: {jugadorActual.PokemonActual.Name}");
            Console.WriteLine($"{jugadorActual.Nombre} elige un pokemon para cambiar al actual: ");
            for (int i = 0; i < jugadorActual.Equipo.Count; i++)
            {
                var pokemon = jugadorActual.Equipo[i];
                if (!pokemon.EstaFueraDeCombate() || pokemon!=jugadorActual.PokemonActual)
                {
                    Console.WriteLine($"{i + 1}: {pokemon.Name} - {pokemon.Health} de vida");
                }
            }
            int PokemonElection = int.Parse(Console.ReadLine());
            jugadorActual.PokemonActual = jugadorActual.Equipo[PokemonElection];
            Console.WriteLine($"Se realizó el cambio correctamente. Su pokemon actual es {jugadorActual.PokemonActual.Name}");
        }
        }
    
}
