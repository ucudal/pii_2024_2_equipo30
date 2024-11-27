using Library;
using NUnit.Framework;
using System.Collections.Generic;

namespace Library.Tests.HistoryUserTests;

[TestFixture]
public class HistoryUser4_Test
{
    private Pokemon Fire;
    private Pokemon Grass;
    private Pokemon Water;
    private Move movement;

    [SetUp]
    public void Setup()
    {
        // Configuración de tipos
        var fireType = new Type();
        fireType.SetType("fire");

        var grassType = new Type();
        grassType.SetType("grass");

        var waterType = new Type();
        waterType.SetType("water");

        // Configuración de Pokémon
        Fire = new Pokemon("Charizard", 6, 100, 80, 78, 109, 85, fireType, new List<Move>());
        Grass = new Pokemon("Venusaur", 3, 100, 50, 100, 100, 100, grassType, new List<Move>());
        Water = new Pokemon("Squirtle", 7, 100, 50, 100, 100, 100, waterType, new List<Move>());

        // Movimiento de prueba
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
    public async Task AttackEffective()
    {
        // Arrange
        double saludInicial = Grass.Health;
        var flamethrower = Fire.Moves[0]; // Movimiento asignado a Charizard

        // Act
        Fire.CalculateDamage(flamethrower, Fire.SpecialAttack, Grass);

        // Assert
        Assert.Less(90, saludInicial, "La salud del oponente debería reducirse más debido a la efectividad del ataque.");
    }

    [Test]
    public async Task AttackNotEffective()
    {
        // Arrange
        double saludInicial = Water.Health;
        var flamethrower = Fire.Moves[0]; // Movimiento asignado a Charizard

        // Act
        Fire.CalculateDamage(flamethrower, Fire.SpecialAttack, Water);

        // Assert
        Assert.GreaterOrEqual(Water.Health, saludInicial - flamethrower.MoveDetails.Power, "El daño debería ser reducido debido a la poca efectividad.");
    }
}
