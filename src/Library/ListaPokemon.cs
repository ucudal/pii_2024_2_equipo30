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
            new Pokemon("Pikachu", 100, movimientos.ObtenerMovimientosAleatorios(), TipoPokemon.Electrico),
            new Pokemon("Charizard", 120, movimientos.ObtenerMovimientosAleatorios(), TipoPokemon.Fuego),
            new Pokemon("Blastoise", 110, movimientos.ObtenerMovimientosAleatorios(), TipoPokemon.Agua),
            new Pokemon("Venusaur", 110, movimientos.ObtenerMovimientosAleatorios(), TipoPokemon.Planta),
            new Pokemon("Gengar", 90, movimientos.ObtenerMovimientosAleatorios(), TipoPokemon.Fantasma),
            new Pokemon("Snorlax", 160, movimientos.ObtenerMovimientosAleatorios(), TipoPokemon.Normal),
            new Pokemon("Machamp", 130, movimientos.ObtenerMovimientosAleatorios(), TipoPokemon.Lucha),
            new Pokemon("Alakazam", 95, movimientos.ObtenerMovimientosAleatorios(), TipoPokemon.Psiquico),
            new Pokemon("Jolteon", 80, movimientos.ObtenerMovimientosAleatorios(), TipoPokemon.Electrico),
            new Pokemon("Lapras", 120, movimientos.ObtenerMovimientosAleatorios(), TipoPokemon.Hielo),
            new Pokemon("Gyarados", 125, movimientos.ObtenerMovimientosAleatorios(), TipoPokemon.Volador),
            new Pokemon("Dragonite", 150, movimientos.ObtenerMovimientosAleatorios(), TipoPokemon.Dragón),
            new Pokemon("Arcanine", 115, movimientos.ObtenerMovimientosAleatorios(), TipoPokemon.Fuego),
            new Pokemon("Umbreon", 95, movimientos.ObtenerMovimientosAleatorios(), TipoPokemon.Oscuro),
            new Pokemon("Tyranitar", 140, movimientos.ObtenerMovimientosAleatorios(), TipoPokemon.Roca)
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

