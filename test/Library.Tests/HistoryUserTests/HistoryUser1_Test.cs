using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Library;
using NUnit.Framework;

namespace Library.Tests.HistoryUserTests;

public class HistoryUser1_Test
{
    [SetUp]
    public void setup()
    {
        
    }

    [Test]
    public void HistoryUser1_Test1()
    {
        //Arrange
        List<Pokemon> listaPokemon = new List<Pokemon>();
        {
            new Pokemon("Pikachu", 35, 100, 55, 40, 90, 50, new Type(), new List<Move> { new Move() });
            new Pokemon("Charizard", 78, 150, 84, 78, 100, 85, new Type(), new List<Move> { new Move() });
            new Pokemon("Bulbasaur", 45, 100, 49, 49, 45, 60, new Type(), new List<Move> { new Move() });
            new Pokemon("Squirtle", 44, 100, 48, 65, 43, 50, new Type(), new List<Move> { new Move() });
            new Pokemon("Jigglypuff", 115, 100, 45, 20, 20, 25, new Type(), new List<Move> { new Move() });
            new Pokemon("Gengar", 60, 100, 65, 60, 110, 75, new Type(), new List<Move> { new Move() });
        }
        Player jugador = new Player("Ernesto_El_entrenador", listaPokemon); //error

        //act
        jugador.ElegirEquipo("Pikachu");
        jugador.ElegirEquipo("Charizard");
        jugador.ElegirEquipo("Bulbasaur");
        jugador.ElegirEquipo("Squirtle");
        jugador.ElegirEquipo("jigglypuff");
        jugador.ElegirEquipo("Gengar");
        
        //assert
        Assert.That(listaPokemon.Count, Is.EqualTo(6));
        Assert.That(listaPokemon.Any(p => p.Name == "Pikachu"), Is.True, "No se encontró a Pikachu en la lista de Pokémon.");
        Assert.That(listaPokemon.Any(p => p.Name == "Charizard"), Is.True, "No se encontró a Charizard en la lista de Pokémon.");
        Assert.That(listaPokemon.Any(p => p.Name == "Bulbazur"), Is.True, "No se encontró a Bulbazur en la lista de Pokémon.");
        Assert.That(listaPokemon.Any(p => p.Name == "Squirtle"), Is.True, "No se encontró a Squirtle en la lista de Pokémon.");
        Assert.That(listaPokemon.Any(p => p.Name == "Jigglypuff"), Is.True, "No se encontró a Jigglypuff en la lista de Pokémon.");
        Assert.That(listaPokemon.Any(p => p.Name == "Gengar"), Is.True, "No se encontró a Gengar en la lista de Pokémon.");
    
    }
}