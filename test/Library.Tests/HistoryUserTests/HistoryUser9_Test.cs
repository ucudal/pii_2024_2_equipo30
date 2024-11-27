namespace Library.Tests.HistoryUserTests;
using NUnit.Framework;
using System.Collections.Generic;
using Library.BotDiscord;


[TestFixture]
public class HistoryUser9_Test
{
    private BotQueuePlayers _botQueuePlayers;
    [SetUp]
    public void SetUp()
    {
        _botQueuePlayers = BotQueuePlayers.GetInstance();
    }

    [Test]
    public void JoinQueue_Test()
    {
        // Arrange
        Player player = new Player(null,"Ash");

        
        // Act
        string result = _botQueuePlayers.JoinQueue(player);
        
        // Assert
        Assert.AreEqual("Ash se ha unido a la cola de batalla.", result);
    }
}
