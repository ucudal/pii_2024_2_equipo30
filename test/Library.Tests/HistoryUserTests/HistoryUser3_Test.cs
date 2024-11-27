namespace Library.Tests.HistoryUserTests;

using Library;
using NUnit.Framework;
using System.Collections.Generic;

public class HistoryUser3_Test
{
    private Pokemon atacante;
    private Pokemon oponente;
    private Move movimiento;

    [SetUp]
    public void Setup()
    {
        // Creamos instancias de Type y establecemos sus valores
        var fireType = new Type();
        fireType.SetType("fire");

        var grassType = new Type();
        grassType.SetType("grass");

        // Crear las instancias de Pokémon usando los tipos configurados
        atacante = new Pokemon("Charizard", 6, 90, 84, 78, 109, 85, fireType, new List<Move>());
        oponente = new Pokemon("Venusaur", 3, 100, 82, 83, 100, 100, grassType, new List<Move>());

        // Creamos un movimiento
        var flamethrower = new Move
        {
            MoveDetails = new MoveDetail
            {
                Name = "Flamethrower",
                Power = 90,
                Accuracy = 100,
                URL = "Nothing"
            }
        };

        // Añadir el movimiento a la lista de movimientos del atacante
        atacante.Moves.Add(flamethrower);
    }
    [Test]
    public async Task DamageTest()
    {
        // Arrange
        double saludInicial = oponente.Health;

        // Act
        atacante.AttackP(null, oponente, atacante.Moves[0], 1, null);

        // Assert
        Assert.Less(atacante.Health, saludInicial, "La salud del oponente debería reducirse después del ataque exitoso.");
        Console.WriteLine($"Salud inicial: {saludInicial}, Salud final: {atacante.Health}");
    }

}