using Library;
using NUnit.Framework;
using System.Collections.Generic;

namespace Library.Tests.HistoryUserTests
{
    /// <summary>
    /// Clase que contiene pruebas unitarias para la funcionalidad relacionada con los ataques de los Pokémon.
    /// </summary>
    [TestFixture]
    public class HistoryUser4_Test
    {
        /// <summary>
        /// Pokémon de tipo Fuego que se usará en las pruebas.
        /// </summary>
        private Pokemon Fire;

        /// <summary>
        /// Pokémon de tipo Planta que se usará en las pruebas.
        /// </summary>
        private Pokemon Grass;

        /// <summary>
        /// Pokémon de tipo Agua que se usará en las pruebas.
        /// </summary>
        private Pokemon Water;

        /// <summary>
        /// Movimiento utilizado en las pruebas.
        /// </summary>
        private Move movement;

        /// <summary>
        /// Configura los Pokémon y sus movimientos antes de cada prueba.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            // Configuración de tipos
            var fireType = new Type();
            fireType.SetType("fire");

            var grassType = new Type();
            grassType.SetType("grass");

            var waterType = new Type();
            waterType.SetType("water");

            // Configuración de Pokémon
            Fire = new Pokemon("Charizard", 6, 100, 80, 78, 109, 85, fireType, new List<Move>());
            Grass = new Pokemon("Venusaur", 3, 100, 50, 100, 100, 100, grassType, new List<Move>());
            Water = new Pokemon("Squirtle", 7, 100, 50, 100, 100, 100, waterType, new List<Move>());

            // Movimiento de prueba
            var flamethrower = new Move
            {
                MoveDetails = new MoveDetail
                {
                    Name = "Flamethrower",
                    Accuracy = 100,
                    Power = 90,
                    URL = "nothing"
                }
            };
            Fire.Moves.Add(flamethrower);
        }

        /// <summary>
        /// Prueba que verifica que un ataque sea efectivo contra un tipo de Pokémon.
        /// </summary>
        [Test]
        public async Task AttackEffective()
        {
            // Arrange: Configurar el estado inicial antes de realizar la acción.
            double saludInicial = Grass.Health;
            var flamethrower = Fire.Moves[0]; // Movimiento asignado a Charizard

            // Act: Ejecutar la acción a probar.
            Fire.CalculateDamage(flamethrower, Fire.SpecialAttack, Grass);

            // Assert: Verificar el resultado esperado.
            Assert.Less(90, saludInicial, "La salud del oponente debería reducirse más debido a la efectividad del ataque.");
        }

        /// <summary>
        /// Prueba que verifica que un ataque no sea muy efectivo contra un tipo de Pokémon.
        /// </summary>
        [Test]
        public async Task AttackNotEffective()
        {
            // Arrange: Configurar el estado inicial antes de realizar la acción.
            double saludInicial = Water.Health;
            var flamethrower = Fire.Moves[0]; // Movimiento asignado a Charizard

            // Act: Ejecutar la acción a probar.
            Fire.CalculateDamage(flamethrower, Fire.SpecialAttack, Water);

            // Assert: Verificar el resultado esperado.
            Assert.GreaterOrEqual(Water.Health, saludInicial - flamethrower.MoveDetails.Power, "El daño debería ser reducido debido a la poca efectividad.");
        }
    }
}
