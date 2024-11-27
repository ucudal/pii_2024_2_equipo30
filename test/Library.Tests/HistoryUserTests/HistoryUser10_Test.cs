namespace Library.Tests.HistoryUserTests
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using Library.BotDiscord;
    using DSharpPlus.Entities;

    /// <summary>
    /// Clase de prueba para verificar la funcionalidad de mostrar la cola de jugadores del bot.
    /// </summary>
    [TestFixture]
    public class HistoryUser10_Test
    {
        /// <summary>
        /// Instancia de la cola de jugadores del bot utilizada para las pruebas.
        /// </summary>
        private BotQueuePlayers botQueuePlayers;

        /// <summary>
        /// Clase derivada de Player para propósitos de prueba.
        /// </summary>
        public class TestPlayer : Player
        {
            /// <summary>
            /// Constructor para la clase de prueba TestPlayer.
            /// </summary>
            /// <param name="namePlayer">Nombre del jugador.</param>
            public TestPlayer(string namePlayer) : base(null, namePlayer, null, false)
            {
            }
        }

        /// <summary>
        /// Método de configuración que se ejecuta antes de cada prueba.
        /// Inicializa la instancia de `BotQueuePlayers`.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            botQueuePlayers = BotQueuePlayers.GetInstance();
        }

        /// <summary>
        /// Prueba que verifica si la lista de jugadores en la cola se muestra correctamente.
        /// </summary>
        [Test]
        public void ShowQueue_Test()
        {
            // Arrange: Configuración inicial del estado antes de la acción a probar.

            // Crear instancias de los jugadores que se van a añadir a la cola
            var player1 = new TestPlayer("Ash");
            var player2 = new TestPlayer("Gary");

            // Unir a los jugadores a la cola
            botQueuePlayers.JoinQueue(player1);
            botQueuePlayers.JoinQueue(player2);

            // Act: Realizar la acción que se va a probar.

            // Obtener el resultado de la visualización de los jugadores en la cola
            var result = botQueuePlayers.MostrarJugadores();

            // Assert: Verificar que el resultado es el esperado.

            // Verificar que los jugadores en la cola se muestran correctamente
            Assert.AreEqual("Jugadores en cola: Ash, Gary", result);
        }
    }
}
