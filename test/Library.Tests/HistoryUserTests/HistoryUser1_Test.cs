using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Library;
using NUnit.Framework;
using NUnit.Framework;
using System.Collections.Generic;
using Library.BotDiscord;



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
        Player jugador = new Player(null,"Ernesto_El_entrenador", listaPokemon); //error

        //act
        listaPokemon.Add(new Pokemon("Pikachu", 35, 100, 55, 40, 90, 50, new Type(), new List<Move> { new Move() }));
        listaPokemon.Add(new Pokemon("Charizard", 78, 150, 84, 78, 100, 85, new Type(), new List<Move> { new Move() }));
        listaPokemon.Add(new Pokemon("Bulbasaur", 45, 100, 49, 49, 45, 60, new Type(), new List<Move> { new Move() }));
        listaPokemon.Add(new Pokemon("Squirtle", 44, 100, 48, 65, 43, 50, new Type(), new List<Move> { new Move() }));
        listaPokemon.Add(new Pokemon("Jigglypuff", 115, 100, 45, 20, 20, 25, new Type(), new List<Move> { new Move() }));
        listaPokemon.Add(new Pokemon("Gengar", 60, 100, 65, 60, 110, 75, new Type(), new List<Move> { new Move() }));

        
        //assert
        Assert.That(listaPokemon.Count, Is.EqualTo(6));
    }
}