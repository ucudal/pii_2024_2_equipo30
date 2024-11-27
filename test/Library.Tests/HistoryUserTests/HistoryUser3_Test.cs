namespace Library.Tests.HistoryUserTests
{
    using Library;
    using NUnit.Framework;
    using System.Collections.Generic;

    /// <summary>
    /// Clase que contiene las pruebas para la funcionalidad de los usuarios en el historial.
    /// </summary>
    public class HistoryUser3_Test
    {
        /// <summary>
        /// Pokémon atacante que será utilizado en las pruebas.
        /// </summary>
        private Pokemon atacante;

        /// <summary>
        /// Pokémon oponente que será utilizado en las pruebas.
        /// </summary>
        private Pokemon oponente;

        /// <summary>
        /// Movimiento utilizado en las pruebas.
        /// </summary>
        private Move movimiento;

        /// <summary>
        /// Método de configuración que se ejecuta antes de cada prueba.
        /// Inicializa los Pokémon y movimientos que se utilizarán en las pruebas.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            // Creamos instancias de Type y establecemos sus valores
            var fireType = new Type();
            fireType.SetType("fire");

            var grassType = new Type();
            grassType.SetType("grass");

            // Crear las instancias de Pokémon usando los tipos configurados
            atacante = new Pokemon("Charizard", 6, 90, 84, 78, 109, 85, fireType, new List<Move>());
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

        /// <summary>
        /// Prueba que verifica que la salud del oponente se reduzca después de que el Pokémon atacante realice un ataque exitoso.
        /// </summary>
        [Test]
        public async Task DamageTest()
        {
            // Arrange: Definir la salud inicial del oponente
            double saludInicial = oponente.Health;

            // Act: El atacante ataca al oponente con su primer movimiento
            atacante.AttackP(null, oponente, atacante.Moves[0], 1, null);

            // Assert: Verificar que la salud del oponente se haya reducido
            Assert.Less(atacante.Health, saludInicial, "La salud del oponente debería reducirse después del ataque exitoso.");
            Console.WriteLine($"Salud inicial: {saludInicial}, Salud final: {atacante.Health}");
        }
    }
}
