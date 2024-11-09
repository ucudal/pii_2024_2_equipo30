namespace Library.Tests;

using Library;
using NUnit.Framework;
using System.Collections.Generic;

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
    public void RegularDealDamage()
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
            SpecialStatus = SpecialStatus.NoneStatus
        };
        // Act
        atacante.AttackP(oponente, movimiento);

        // Assert
        Assert.Less(oponente.Health, saludInicial, "La salud del oponente debería reducirse después del ataque exitoso.");
        Console.WriteLine(oponente.Health);
    }

    [Test]
    public void SleepyCantAttack()
    //Este metodo testea que si el atacante esta dormido no puede inflingir daño al oponente
    {
        // Arrange
        atacante.Status = SpecialStatus.Asleep;
        double saludInicial = oponente.Health;

        // Act
        atacante.AttackP(oponente, movimiento);

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
            SpecialStatus = SpecialStatus.Paralyzed
        };

        // Act
        atacante.AttackP(oponente, movimiento);

        // Assert
        Assert.AreEqual(SpecialStatus.Paralyzed, oponente.Status, "El estado 'Paralyzed' debería aplicarse al oponente.");
    }

    [Test]
    public void SameState()
    //En este metodo probamos que si un pokemon ya esta afectado por un estado, que no pueda ser afectado por otro
    {
        // Arrange
        oponente.Status = SpecialStatus.Asleep;
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
            SpecialStatus = SpecialStatus.Paralyzed
        };

        // Act
        atacante.AttackP(oponente, movimiento);

        // Assert
        Assert.AreEqual(SpecialStatus.Asleep, oponente.Status, "El estado del oponente no debería cambiar si ya tiene otro estado aplicado.");
    }

    [Test]
    public void AtacarCalculaDañoCorrectamenteConEfectividad()
    {
        // Arrange
        oponente.Type.SetType("grass");  // Configura el tipo del oponente
        atacante.Type.SetType("fire");   // Configura el tipo del atacante, efectivo contra "grass"

        // Act
        atacante.AttackP(oponente, movimiento);

        // Assert
        Assert.Less(oponente.Health, 100, "La salud del oponente debería reducirse tomando en cuenta la efectividad del tipo.");
    }
    [Test]
    public void True_OutOfCombat()
    {
        // Arrange
        var pokemon = new Pokemon { Health = 0 };

        // Act
        bool resultado = pokemon.OutOfAction();

        // Assert
        Assert.IsTrue(resultado, "El Pokémon debería estar fuera de combate cuando la salud es 0.");
        Assert.IsTrue(pokemon.Outofaction, "La propiedad Outofaction debería ser verdadera.");
    }
    [Test]
    public void False_OutOfCombat()
    {
        // Arrange
        var pokemon = new Pokemon { Health = 1 };

        // Act
        bool resultado = pokemon.OutOfAction();

        // Assert
        Assert.IsFalse(resultado, "El Pokémon NO debería estar fuera de combate cuando la salud es mayor a 0.");
        Assert.IsFalse(pokemon.Outofaction, "La propiedad Outofaction debería ser falsa."); //ksba
    }
    [Test]
    public void PoisonAttack()
    {
        oponente.Status = SpecialStatus.NoneStatus;
        // Arrange
        //Power is 0, however since opponent is now poisoned, he should lose 5% health
        var poisonMan = new MoveDetail
        {
            Name = "StingRay",
            Power = 0,
            Accuracy = 100,
            URL = "http://example.com/move/Generic?"
        };
        movimiento = new Move
        {
            MoveDetails = poisonMan,
            SpecialStatus = SpecialStatus.Poisoned
        };

        // Act
        atacante.AttackP(oponente, movimiento);

        // Assert
        Assert.AreEqual(95,oponente.Health, "El oponente debería perder un 5% de su salud debido a estar envenenado.");
        Console.WriteLine(atacante.Attack);
    }
    [Test]
    public void BurningAttack()
    {
        oponente.Status = SpecialStatus.NoneStatus;
        // Arrange
        //Power is 0, however since opponent is now burned, he should lose 10% health
        var burningMan = new MoveDetail
        {
            Name = "HellBlaze",
            Power = 0,
            Accuracy = 100,
            URL = "http://example.com/move/Generic?"
        };
        movimiento = new Move
        {
            MoveDetails = burningMan,
            SpecialStatus = SpecialStatus.Burned
        };

        // Act
        atacante.AttackP(oponente, movimiento);

        // Assert
        Assert.AreEqual(90,oponente.Health, "El oponente debería perder un 10% de su salud debido a estar quemado.");
        Console.WriteLine(atacante.Attack);
    }

}
