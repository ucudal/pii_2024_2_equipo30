namespace Library.Tests.HistoryUserTests
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using Library.BotDiscord;

    /// <summary>
    /// Clase de prueba para verificar la funcionalidad de unirse a la cola de batalla del bot.
    /// </summary>
    [TestFixture]
    public class HistoryUser9_Test
    {
        /// <summary>
        /// Instancia de la cola de jugadores del bot utilizada para las pruebas.
        /// </summary>
        private BotQueuePlayers _botQueuePlayers;

        /// <summary>
        /// Método de configuración que se ejecuta antes de cada prueba.
        /// Inicializa la instancia de `BotQueuePlayers`.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _botQueuePlayers = BotQueuePlayers.GetInstance();
        }

        /// <summary>
        /// Prueba que verifica si un jugador puede unirse correctamente a la cola de batalla.
        /// </summary>
        [Test]
        public void JoinQueue_Test()
        {
            // Arrange: Configuración inicial del estado antes de la acción a probar.

            // Crear una instancia del jugador que se va a añadir a la cola
            Player player = new Player(null, "Ash");

            // Act: Realizar la acción que se va a probar.

            // El jugador se une a la cola de batalla
            string result = _botQueuePlayers.JoinQueue(player);

            // Assert: Verificar que el resultado es el esperado.

            // Verificar que el mensaje de respuesta es el correcto
            Assert.AreEqual("Ash se ha unido a la cola de batalla.", result);
        }
    }
}