namespace Library;

using System;
using System.Collections.Generic;

public class ListaPokemon
{
    public List<Pokemon> ListaDePokemons { get; private set; }

    public ListaPokemon(Movimientos movimientos)
    {
        ListaDePokemons = new List<Pokemon>
        {
            new Pokemon("Pikachu", 100, movimientos.ObtenerMovimientosAleatorios()),
            new Pokemon("Charizard", 120, movimientos.ObtenerMovimientosAleatorios()),
            new Pokemon("Blastoise", 110, movimientos.ObtenerMovimientosAleatorios()),
            new Pokemon("Venusaur", 110, movimientos.ObtenerMovimientosAleatorios()),
            new Pokemon("Gengar", 90, movimientos.ObtenerMovimientosAleatorios()),
            new Pokemon("Snorlax", 160, movimientos.ObtenerMovimientosAleatorios()),
            new Pokemon("Machamp", 130, movimientos.ObtenerMovimientosAleatorios()),
            new Pokemon("Alakazam", 95, movimientos.ObtenerMovimientosAleatorios()),
            new Pokemon("Jolteon", 80, movimientos.ObtenerMovimientosAleatorios()),
            new Pokemon("Lapras", 120, movimientos.ObtenerMovimientosAleatorios()),
            new Pokemon("Gyarados", 125, movimientos.ObtenerMovimientosAleatorios()),
            new Pokemon("Dragonite", 150, movimientos.ObtenerMovimientosAleatorios()),
            new Pokemon("Arcanine", 115, movimientos.ObtenerMovimientosAleatorios()),
            new Pokemon("Umbreon", 95, movimientos.ObtenerMovimientosAleatorios()),
            new Pokemon("Tyranitar", 140, movimientos.ObtenerMovimientosAleatorios())
        };
    }

    // Método para obtener una lista aleatoria de 6 pokémon
    public List<Pokemon> ObtenerEquipoAleatorio()
    {
        Random random = new Random();
        List<Pokemon> equipoAleatorio = new List<Pokemon>();

        while (equipoAleatorio.Count < 6)
        {
            int index = random.Next(ListaDePokemons.Count);
            Pokemon pokemonSeleccionado = ListaDePokemons[index];

            if (!equipoAleatorio.Contains(pokemonSeleccionado))
            {
                equipoAleatorio.Add(pokemonSeleccionado);
            }
        }

        return equipoAleatorio;
    }
}

