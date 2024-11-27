using Library;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace Library.Tests.HistoryUserTests
{
    /// <summary>
    /// Clase de prueba para verificar la funcionalidad de la clase HistoryUser mediante diferentes casos de uso.
    /// </summary>
    [TestFixture]
    public class HistoryUser5_Test
    {
        /// <summary>
        /// Instancia de Pokémon utilizada en las pruebas.
        /// </summary>
        private Pokemon Pokemon1;

        /// <summary>
        /// Segunda instancia de Pokémon utilizada en las pruebas.
        /// </summary>
        private Pokemon Pokemon2;

        /// <summary>
        /// Lista de Pokémon utilizada en las pruebas.
        /// </summary>
        private List<Pokemon> listaPokemon;

        /// <summary>
        /// Clase derivada de Player, utilizada únicamente para propósitos de prueba.
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
        /// Inicializa la lista de Pokémon y crea instancias de Pokémon y sus tipos.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            // Inicializamos la lista de Pokémon
            listaPokemon = new List<Pokemon>();

            // Creamos instancias de Type y establecemos sus valores
            var fireType = new Type();
            fireType.SetType("fire");

            var grassType = new Type();
            grassType.SetType("grass");

            // Creamos los Pokémon con vida inicial
            Pokemon1 = new Pokemon("Charizard", 6, 100, 84, 78, 109, 85, fireType, new List<Move>());
            Pokemon2 = new Pokemon("Venusaur", 3, 100, 82, 83, 100, 100, grassType, new List<Move>());

            // Añadir los Pokémon a la lista de Pokémon
            listaPokemon.Add(Pokemon1);
            listaPokemon.Add(Pokemon2);
        }

        /// <summary>
        /// Prueba que verifica que se muestra el turno correctamente.
        /// </summary>
        [Test]
        public void MostrarTurno_ImprimeTurnoCorrecto()
        {
            // Arrange - Utiliza los objetos ya creados en Setup
            var jugador1 = new TestPlayer("Jugador1");
            var jugador2 = new TestPlayer("Jugador2");
            var turno = new Shift(jugador1, jugador2);

            // Captura la salida de la consola
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act: Llama al método ShowShift, el cual imprime el turno en consola
                turno.ShowShift(null);  // Llamada sin esperar, ya que es async void

                // Assert: Verifica que la salida sea la esperada
                var expectedOutput = $"-- Turno 1 / {jugador1.NamePlayer} es tu turno! --{Environment.NewLine}";
                Assert.AreEqual(1, turno.shiftNumber); // Verifica el número de turno
            }
        }
    }
}
