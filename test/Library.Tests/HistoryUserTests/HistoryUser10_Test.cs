namespace Library.Tests.HistoryUserTests;
using NUnit.Framework;
using System.Collections.Generic;
using Library.BotDiscord;
using DSharpPlus.Entities;
using NUnit.Framework;
using Library.BotDiscord;

[TestFixture]
public class HistoryUser10_Test
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
        botQueuePlayers = BotQueuePlayers.GetInstance();

    }
    [Test]
    public void ShowQueue_Test()
    {
        // Arrange
        var player1 = new TestPlayer("Ash");
        var player2 = new TestPlayer("Gary");

        botQueuePlayers.JoinQueue(player1);
        botQueuePlayers.JoinQueue(player2);

        // Act
        var result = botQueuePlayers.MostrarJugadores();

        // Assert
        Assert.AreEqual("Jugadores en cola: Ash, Gary", result);
    }
}




