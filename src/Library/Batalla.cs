using System.Threading.Channels;
using System.Collections.Generic;

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
        Console.WriteLine($"{jugadorActual.Nombre}, elige un Pokémon para atacar:");

        // Mostrar Pokémon del jugador
        for (int i = 0; i < jugadorActual.Equipo.Count; i++)
        {
            var pokemon = jugadorActual.Equipo[i]; 
            if (!pokemon.EstaFueraDeCombate())
            {
                Console.WriteLine($"{i + 1}: {pokemon.Name} - {pokemon.Health} de vida");
            }
        }
        // Elección del Pokémon
        int eleccion = int.Parse(Console.ReadLine()) - 1;
        if (jugadorActual.Equipo == null || jugadorActual.Equipo.Count == 0)
        {
            Console.WriteLine($"{jugadorActual.Nombre} no tiene Pokémon disponibles para luchar.");
            return;
        }
        
        Pokemon pokemonSeleccionado = jugadorActual.Equipo[eleccion];
        if (pokemonSeleccionado.Moves == null || pokemonSeleccionado.Moves.Count == 0)
        {
            Console.WriteLine($"{pokemonSeleccionado.Name} no tiene movimientos disponibles.");
            return;
        }
        
        if (!pokemonSeleccionado.PuedeAtacar())
        {
            Console.WriteLine($"{pokemonSeleccionado.Name} no puede atacar este turno.");
            return;
        }
        // Mostrar movimientos del Pokémon seleccionado
        Console.WriteLine($"{pokemonSeleccionado.Name}, elige un movimiento:");

        for (int i = 0; i < pokemonSeleccionado.Moves.Count; i++)
        {
            var movimiento = pokemonSeleccionado.Moves[i];
            Console.WriteLine($"{i + 1}: {movimiento.MoveDetails.Name} (Poder: {movimiento.MoveDetails.Power}) (Precisión: {movimiento.MoveDetails.Accuracy}) Especial: {movimiento.EstadoEspecial}");
        }

        // Elección del movimiento
        int movimientoSeleccionado = int.Parse(Console.ReadLine()) - 1;

        // Opción de usar un ítem antes de atacar
        Console.WriteLine("¿Quieres usar un ítem antes de atacar? (sí/no)");
        string respuesta = Console.ReadLine()?.ToLower();

        if (respuesta == "sí")
        {
            UsarItem(jugadorActual, pokemonSeleccionado);
        }

        // Realizar el ataque
        pokemonSeleccionado.Atacar(jugadorActual.Equipo[0], jugadorOponente.Equipo[0], pokemonSeleccionado.Moves[movimientoSeleccionado]); // Suponiendo que el oponente es el primer Pokémon

        // Opción de usar un ítem después de atacar
        if (!jugadorOponente.TodosFueraDeCombate()) // Solo si el oponente sigue en combate
        {
            Console.WriteLine($"¿Estas listo {jugadorActual}? (sí/no)");
            respuesta = Console.ReadLine()?.ToLower();

            if (respuesta == "sí")
            {
                // Necesitamos pausar para garantizar que el ítem pueda ser usado antes del siguiente turno
                System.Threading.Thread.Sleep(500); // Pausa para darle tiempo al jugador a usar el ítem antes del siguiente turno
                UsarItem(jugadorActual, pokemonSeleccionado);
            }
        }
    }

    private void UsarItem(Jugador jugador, Pokemon pokemon)
    {
        if (jugador.Inventario == null || jugador.Inventario.Count == 0)
        {
            Console.WriteLine("No tienes ítems disponibles.");
            return;
        }

        Console.WriteLine("Elige un ítem para usar:");
        for (int i = 0; i < jugador.Inventario.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {jugador.Inventario[i].ItemsName} - {jugador.Inventario[i].ItemsDescription}");
        }

        int eleccion = int.Parse(Console.ReadLine()) - 1;
        if (eleccion < 0 || eleccion >= jugador.Inventario.Count)
        {
            Console.WriteLine("Elección inválida.");
            return;
        }

        IItem itemSeleccionado = jugador.Inventario[eleccion];
        itemSeleccionado.Use(pokemon);
        itemSeleccionado.Consume();
    }
}