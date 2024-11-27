using Library;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace Library.Tests.HistoryUserTests
{
    [TestFixture]
    public class HistoryUser5_Test
    {
        private Pokemon Pokemon1;
        private Pokemon Pokemon2;
        private List<Pokemon> listaPokemon;
        public class TestPlayer : Player
        {
            // Constructor alternativo para pruebas
            public TestPlayer(string namePlayer) : base(null, namePlayer, null, false)
            {
            }
        }

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
            listaPokemon.Add(Pokemon1);
            listaPokemon.Add(Pokemon2);
        }

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

                // Act
                turno.ShowShift(null);  // Llamada sin esperar, ya que es async void

                // Assert
                var expectedOutput = $"-- Turno 1 / {jugador1.NamePlayer} es tu turno! --{Environment.NewLine}";
                Assert.AreEqual(expectedOutput, sw.ToString());
                Assert.AreEqual(1, turno.shiftNumber); // Verifica el número de turno
            }
        }

    }
}

