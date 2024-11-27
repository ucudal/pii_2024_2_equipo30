namespace Library.Tests.HistoryUserTests
{
    using NUnit.Framework;
    using Library.BotDiscord;
    using System.Collections.Generic;

    /// <summary>
    /// Clase de prueba para verificar la funcionalidad de iniciar una batalla entre jugadores en la cola de espera del bot.
    /// </summary>
    [TestFixture]
    public class HistoryUser11_Test
    {
        /// <summary>
        /// Instancia de la cola de jugadores del bot utilizada para las pruebas.
        /// </summary>
        private BotQueuePlayers botQueuePlayers;

        /// <summary>
        /// Clase derivada de Player utilizada únicamente con fines de prueba.
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
        /// Restablece la instancia de `BotQueuePlayers` para asegurar una nueva configuración en cada prueba.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            // Restablecer la instancia del singleton de BotQueuePlayers para garantizar una configuración limpia.
            typeof(BotQueuePlayers)
                .GetField("_instance", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                ?.SetValue(null, null);

            // Obtener una nueva instancia de BotQueuePlayers.
            botQueuePlayers = BotQueuePlayers.GetInstance();
        }

        /// <summary>
        /// Prueba que verifica si dos jugadores en la cola pueden iniciar una batalla correctamente.
        /// </summary>
        [Test]
        public void StartBattle_Test()
        {
            // Arrange: Configuración del estado inicial.

            // Crear dos jugadores de prueba.
            var player1 = new TestPlayer("Jugador1");
            var player2 = new TestPlayer("Jugador2");

            // Ambos jugadores se unen a la cola.
            botQueuePlayers.JoinQueue(player1);
            botQueuePlayers.JoinQueue(player2);

            // Act: Realizar la acción que se va a probar.

            // Obtener los próximos jugadores en la cola.
            var result = botQueuePlayers.ObtenerProximosJugadores();

            // Simular el mensaje de batalla entre los jugadores.
            var battleMessage = $"{result[0].NamePlayer} y {result[1].NamePlayer} han comenzado una batalla!";

            // Assert: Verificar que el resultado sea el esperado.

            // Verificar que el mensaje de batalla sea el correcto.
            Assert.AreEqual("Jugador1 y Jugador2 han comenzado una batalla!", battleMessage);

            // Adicional: Verificar que no haya jugadores restantes en la cola.
            var remainingPlayers = botQueuePlayers.MostrarJugadores();
            Assert.AreEqual("No hay jugadores en la cola.", remainingPlayers);
        }
    }
}
