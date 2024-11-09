using Library;
using NUnit.Framework;
using System.Collections.Generic;

namespace Library.Tests.HistoryUserTests;

public class HistoryUser4_Test
{
    private Pokemon Fire;
    private Pokemon Grass;
    private Pokemon Water;
    private Move movement;

    [SetUp]
    public void Setup()
    {
        var fireType = new Type();
        fireType.SetType("fire");

        var grassType = new Type();
        grassType.SetType("grass");

        var waterType = new Type();
        waterType.SetType("water");
        
        Fire = new Pokemon("Charizard", 6, 100, 80, 78, 109, 85, fireType, new List<Move>());
        
        Grass = new Pokemon("Venusaur", 3, 100,50 , 100, 100, 100, grassType, new List<Move>());
        Water = new Pokemon("Squirtle",7,100,50, 100,100,100,waterType,new List<Move>());
        
        
        var flamethrower = new Move
        {
            MoveDetails = new MoveDetail
            {
                Name = "Flamethrower",
                Accuracy = 100,
                Power = 90,
                URL = "nothing"
            }
        };
        Fire.Moves.Add(flamethrower);
    }
    
    [Test]
    public void AtackEfective()
    {
        // Arrange
        double saludInicial2 = Grass.Health; 
        var flamethrower = new MoveDetail 
        {
                Name = "Flamethrower",
                Power = 90,
                Accuracy = 100,
                URL = "Nothing"
        };
        movement = new Move
        {
                MoveDetails = flamethrower,
                EspecialStatus = EspecialStatus.NoneStatus
        };
        // Act
        Fire.CalculateDamage( movement,100,Grass);

        // Assert
        Assert.Less(Grass.Health, saludInicial2, "La salud del oponente debería reducirse mas segun la efectividad.");
        Console.WriteLine(Grass.Health);
    }

    [Test]
    public void AtackNotEfective()
    {
        
        // Arrange
        double saludInicial = Water.Health; 
        
        var flamethrower = new MoveDetail 
        {
            Name = "Flamethrower",
            Power = 90,
            Accuracy = 100,
            URL = "Nothing"
        };
        movement = new Move
        {
            MoveDetails = flamethrower,
            EspecialStatus = EspecialStatus.NoneStatus
        };
        // Act
        Fire.AttackP(Water, movement);

        // Assert
        Assert.Less(Water.Health, saludInicial, "La salud del oponente debería reducirse mas segun la efectividad.");
        Console.WriteLine(Water.Health);
    }
}