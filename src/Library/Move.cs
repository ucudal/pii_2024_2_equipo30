using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library
{
    /// <summary>
    /// Clase que representa un movimiento que un Pokémon puede realizar durante el combate.
    /// Contiene detalles sobre el movimiento y su estado especial.
    /// </summary>
    public class Move
    {
        /// <summary>
        /// Detalles del movimiento, como nombre, poder, y precisión.
        /// Utiliza una propiedad JSON para mapear el nombre del atributo "move".
        /// </summary>
        [JsonPropertyName("move")]
        public MoveDetail MoveDetails { get; set; }

        /// <summary>
        /// Lista de movimientos relacionados (si se aplica).
        /// </summary>
        public List<Move> ListMove { get; set; }

        /// <summary>
        /// Estado especial del movimiento que puede afectar al Pokémon oponente, como Parálisis, Quemadura, Veneno, Sueño, etc.
        /// </summary>
        public SpecialStatus SpecialStatus { get; set; }

        /// <summary>
        /// Propiedad que indica si el movimiento es un ataque especial.
        /// Un ataque se considera especial si tiene uno de los estados especiales definidos (Veneno, Quemadura, Sueño, Parálisis).
        /// </summary>
        public bool EspecialAttack =>
            SpecialStatus == SpecialStatus.Poisoned ||
            SpecialStatus == SpecialStatus.Burned ||
            SpecialStatus == SpecialStatus.Asleep ||
            SpecialStatus == SpecialStatus.Paralyzed;

        /// <summary>
        /// Constructor de la clase "Move".
        /// Inicializa un movimiento con un estado especial especificado, que por defecto es "NoneStatus" (sin estado especial).
        /// </summary>
        /// <param name="SpecialStatus">Estado especial del movimiento, por defecto es "NoneStatus".</param>
        public Move(SpecialStatus SpecialStatus = SpecialStatus.NoneStatus)
        {
            this.SpecialStatus = SpecialStatus;
        }
    }
}
