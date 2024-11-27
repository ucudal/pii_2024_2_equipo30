using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Library;
using NUnit.Framework;

namespace Library.Tests
{
    /// <summary>
    /// Clase de prueba para verificar la funcionalidad de los ítems como SuperPoción, TotalCure y Revive.
    /// </summary>
    public class ItemsTests
    {
        /// <summary>
        /// Instancia del Pokémon utilizada en las pruebas.
        /// </summary>
        private Pokemon _pokemon;

        /// <summary>
        /// Instancia de SuperPoción utilizada en las pruebas.
        /// </summary>
        private SuperPotion _superPotion;

        /// <summary>
        /// Instancia de TotalCure utilizada en las pruebas.
        /// </summary>
        private TotalCure _totalCure;

        /// <summary>
        /// Instancia de Revive utilizada en las pruebas.
        /// </summary>
        private Revive _revive;

        /// <summary>
        /// Método de configuración que se ejecuta antes de cada prueba.
        /// Inicializa una instancia de Pokémon para las pruebas de ítems.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _pokemon = new Pokemon("Pikachu", 1, 100, 55, 40, 50, 50, new Type(), new List<Move>());
            _pokemon.MaxHealt = 200;
        }

        /// <summary>
        /// Prueba que verifica si la SuperPoción cura correctamente al Pokémon.
        /// </summary>
        [Test]
        public void SuperPotion_Heal()
        {
            // Asserts: Configuración inicial.
            _pokemon.MaxHealt = 100;
            _pokemon.Health = 50;

            var superPotion = new SuperPotion(quantity: 1, hpRecovered: 60);

            // Act: Uso de la SuperPoción para curar al Pokémon.
            superPotion.Use(_pokemon);

            // Assert: Verificar si la salud del Pokémon se restablece al máximo.
            Assert.That(_pokemon.Health, Is.EqualTo(100));
        }

        /// <summary>
        /// Prueba que verifica si TotalCure cura correctamente el estado envenenado del Pokémon.
        /// </summary>
        [Test]
        public void TotalCure_poison()
        {
            // Arrange: Configuración inicial del estado de envenenamiento.
            _pokemon.Status = SpecialStatus.Poisoned;
            var totalCure = new TotalCure(quantity: 1);

            // Act: Uso de TotalCure para curar el estado del Pokémon.
            totalCure.Use(_pokemon);

            // Assert: Verificar si el estado se ha restablecido a ninguno.
            Assert.That(_pokemon.Status, Is.EqualTo(SpecialStatus.NoneStatus));
        }

        /// <summary>
        /// Prueba que verifica si TotalCure cura correctamente el estado de quemadura del Pokémon.
        /// </summary>
        [Test]
        public void TotalCure_burned()
        {
            // Arrange: Configuración inicial del estado de quemadura.
            _pokemon.Status = SpecialStatus.Burned;
            var totalCure = new TotalCure(quantity: 1);

            // Act: Uso de TotalCure para curar el estado del Pokémon.
            totalCure.Use(_pokemon);

            // Assert: Verificar si el estado se ha restablecido a ninguno.
            Assert.That(_pokemon.Status, Is.EqualTo(SpecialStatus.NoneStatus));
        }

        /// <summary>
        /// Prueba que verifica si TotalCure cura correctamente el estado de parálisis del Pokémon.
        /// </summary>
        [Test]
        public void TotalCure_Paralized()
        {
            // Arrange: Configuración inicial del estado de parálisis.
            _pokemon.Status = SpecialStatus.Paralyzed;
            var totalCure = new TotalCure(quantity: 1);

            // Act: Uso de TotalCure para curar el estado del Pokémon.
            totalCure.Use(_pokemon);

            // Assert: Verificar si el estado se ha restablecido a ninguno.
            Assert.That(_pokemon.Status, Is.EqualTo(SpecialStatus.NoneStatus));
        }

        /// <summary>
        /// Prueba que verifica que TotalCure no cambie el estado de dormido del Pokémon.
        /// </summary>
        [Test]
        public void TotalCure_Dormido()
        {
            // Arrange: Configuración inicial del estado dormido.
            _pokemon.Status = SpecialStatus.Asleep;
            var totalCure = new TotalCure(quantity: 1);

            // Act: Uso de TotalCure para intentar curar el estado de sueño.
            totalCure.Use(_pokemon);

            // Assert: Verificar si el estado sigue siendo dormido.
            Assert.That(_pokemon.Status, Is.EqualTo(SpecialStatus.Asleep));
        }

        /// <summary>
        /// Prueba que verifica si el ítem Revive puede revivir correctamente al Pokémon.
        /// </summary>
        [Test]
        public void Revive_CanRevive()
        {
            // Arrange: Configuración inicial del Pokémon fuera de combate.
            _pokemon.MaxHealt = 200;
            _pokemon.Outofaction = true;
            _pokemon.Health = 0;
            var revive = new Revive(quantity: 1);

            // Act: Uso del Revive para revivir al Pokémon.
            revive.Use(_pokemon);

            // Assert: Verificar que el Pokémon ya no está fuera de combate y se ha curado al 50% de la salud máxima.
            Assert.That(_pokemon.Outofaction, Is.False);
            Assert.That(_pokemon.Health, Is.EqualTo(100));
        }

        /// <summary>
        /// Prueba que verifica que el ítem Revive no se puede usar si el Pokémon no está fuera de combate.
        /// </summary>
        [Test]
        public void Revive_isnotdead()
        {
            // Arrange: Configuración inicial del Pokémon no fuera de combate.
            _pokemon.MaxHealt = 200;
            _pokemon.Outofaction = false;

            var revive = new Revive(quantity: 1);

            // Assert: Verificar que el Pokémon no está fuera de combate y que la cantidad de Revive no ha cambiado.
            Assert.That(_pokemon.Outofaction, Is.False);
            Assert.That(100, Is.EqualTo(_pokemon.Health));
            Assert.That(revive.Quantity, Is.EqualTo(1));
        }
    }
}
