using System.Collections.Generic;
using System;
namespace Library;

public class EntrenadorPokemon
{
    public string Nombre { get; set; } //Nombre del entrenador
    public List <Pokemon> Pokemons { get; set; } //Lista de pokemon del entrenador

    public EntrenadorPokemon (string nombre) 
    {
        Nombre = nombre;
        Pokemons = new List<Pokemon>();
    }

    public void ElegirPokemon(int index)
    {
        if (index >= 0 && index < Pokemons.Count)
        {
            Console.WriteLine($"{Nombre} ha elegido a {Pokemons[index].Nombre}.");
        }
        else
        {
            Console.WriteLine("Índice de Pokémon inválido.");
        }
    }
}