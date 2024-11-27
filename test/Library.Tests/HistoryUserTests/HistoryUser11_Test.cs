namespace Library.Tests.HistoryUserTests;

using NUnit.Framework;
using Library.BotDiscord;
using System.Collections.Generic;
using NUnit.Framework;
using Library.BotDiscord;

[TestFixture]
public class HistoryUser11_Test
{
    private BotQueuePlayers botQueuePlayers;

    public class TestPlayer : Player
    {
        // Constructor alternativo para pruebas
        public TestPlayer(string namePlayer) : base(null, namePlayer, null, false)
        {
        }
    }

    [SetUp]
    public void SetUp()
    {
        typeof(BotQueuePlayers)
            .GetField("_instance", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
            ?.SetValue(null, null);

        botQueuePlayers = BotQueuePlayers.GetInstance();
    }

    [Test]
    public void StartBattle_Test()
    {
        // Arrange
        var player1 = new TestPlayer("Jugador1");
        var player2 = new TestPlayer("Jugador2");

        // Ambos jugadores se unen a la cola.
        botQueuePlayers.JoinQueue(player1);
        botQueuePlayers.JoinQueue(player2);

        // Act
        var result = botQueuePlayers.ObtenerProximosJugadores();

        // Simula la batalla iniciada entre los jugadores obtenidos.
        var battleMessage = $"{result[0].NamePlayer} y {result[1].NamePlayer} han comenzado una batalla!";

        // Assert
        Assert.AreEqual("Jugador1 y Jugador2 han comenzado una batalla!", battleMessage);

        // Adicional: Verifica que los jugadores ya no están en la cola.
        var remainingPlayers = botQueuePlayers.MostrarJugadores();
        Assert.AreEqual("No hay jugadores en la cola.", remainingPlayers);
    }
}


