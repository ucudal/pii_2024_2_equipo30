namespace Library.Tests;

using Library;
using NUnit.Framework;
using System.Collections.Generic;

using NUnit.Framework;
using System.Collections.Generic;
using Library;

[TestFixture]
public class PokemonTests
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
    public void DealDamage()
    {
        // Arrange
        double saludInicial = oponente.Health;

        // Act
        atacante.Atacar(atacante, oponente, movimiento);

        // Assert
        Assert.Less(oponente.Health, saludInicial, "La salud del oponente debería reducirse después del ataque exitoso.");
    }

    [Test]
    public void SleepyCantAttack()
    //Este metodo testea que si el atacante esta dormido no puede inflingir daño al oponente
    {
        // Arrange
        atacante.Estado = EstadoEspecial.Dormido;
        double saludInicial = oponente.Health;

        // Act
        atacante.Atacar(atacante, oponente, movimiento);

        // Assert
        Assert.AreEqual(saludInicial, oponente.Health, "La salud del oponente no debería cambiar cuando el atacante está dormido.");
    }

    [Test]
    public void ApplyParalyzedState()
    {
        // Arrange
        var paralizarMoveDetail = new MoveDetail
        {
            Name = "Thunder Wave",
            Power = 0,
            Accuracy = 100,
            URL = "http://example.com/move/thunderwave"
        };
        movimiento = new Move
        {
            MoveDetails = paralizarMoveDetail,
            EstadoEspecial = EstadoEspecial.Paralizado
        };

        // Act
        atacante.Atacar(atacante, oponente, movimiento);

        // Assert
        Assert.AreEqual(EstadoEspecial.Paralizado, oponente.Estado, "El estado 'Paralizado' debería aplicarse al oponente.");
    }

    [Test]
    public void SameState()
    //En este metodo probamos que si un pokemon ya esta afectado por un estado, que no pueda ser afectado por otro
    {
        // Arrange
        oponente.Estado = EstadoEspecial.Dormido;
        var paralizarMoveDetail = new MoveDetail
        {
            Name = "Thunder Wave",
            Power = 0,
            Accuracy = 100,
            URL = "http://example.com/move/thunderwave"
        };
        movimiento = new Move
        {
            MoveDetails = paralizarMoveDetail,
            EstadoEspecial = EstadoEspecial.Paralizado
        };

        // Act
        atacante.Atacar(atacante, oponente, movimiento);

        // Assert
        Assert.AreEqual(EstadoEspecial.Dormido, oponente.Estado, "El estado del oponente no debería cambiar si ya tiene otro estado aplicado.");
    }

    [Test]
    public void Atacar_CalculaDañoCorrectamenteConEfectividad()
    {
        // Arrange
        oponente.Type.SetType("grass");  // Configura el tipo del oponente
        atacante.Type.SetType("fire");   // Configura el tipo del atacante, efectivo contra "grass"

        // Act
        atacante.Atacar(atacante, oponente, movimiento);

        // Assert
        Assert.Less(oponente.Health, 100, "La salud del oponente debería reducirse tomando en cuenta la efectividad del tipo.");
    }

}
