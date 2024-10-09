namespace Program;
using Library;

using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Crear la lista de movimientos
        Movimientos movimientos = new Movimientos();

        // Crear los Pokémon disponibles
        ListaPokemon pokemonsDisponibles = new ListaPokemon(movimientos);

        // Crear dos jugadores
        Jugador jugador1 = new Jugador("Jugador 1", pokemonsDisponibles.ObtenerEquipoAleatorio());
        Jugador jugador2 = new Jugador("Jugador 2", pokemonsDisponibles.ObtenerEquipoAleatorio());

        // Crear la batalla
        Batalla batalla = new Batalla(jugador1, jugador2);

        // Iniciar la batalla
        batalla.IniciarBatalla();
    }
}

