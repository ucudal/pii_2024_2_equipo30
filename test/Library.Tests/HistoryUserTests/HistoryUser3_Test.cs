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
        atacante = new Pokemon("Charizard", 6, 100, 84, 78, 109, 85, fireType, new List<Move>());
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
    public void Damage()
    {
        // Arrange
        double saludInicial = oponente.Health;
        var flamethrower = new MoveDetail
        {
            Name = "Flamethrower",
            Power = 90,
            Accuracy = 100,
            URL = "Nothing"
        };
        movimiento = new Move
        {
            MoveDetails = flamethrower,
            EspecialStatus = EspecialStatus.NoneStatus
        };
        // Act
        List<Pokemon> listaPokemon = new List<Pokemon>();
        listaPokemon.Add(atacante);
        atacante.AttackP(new Player("Ernesto_El_entrenador", listaPokemon),oponente,movimiento,1);

        // Assert
        Assert.Less(oponente.Health, saludInicial,"La salud del oponente debería reducirse después del ataque exitoso.");
        Console.WriteLine(oponente.Health);
    }
}