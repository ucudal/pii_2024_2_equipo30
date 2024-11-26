using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library
{
    /// <summary>
    /// Clase que representa un movimiento que un Pokémon puede realizar durante una batalla.
    /// </summary>
    public class Move
    {
        /// <summary>
        /// Detalles del movimiento específico. Esta información se obtiene de la API.
        /// </summary>
        [JsonPropertyName("move")]
        public MoveDetail MoveDetails { get; set; }

        /// <summary>
        /// Lista de movimientos. Puede contener múltiples movimientos que el Pokémon conoce.
        /// </summary>
        public List<Move> ListMove { get; set; }

        /// <summary>
        /// Estado especial asociado al movimiento (por ejemplo, envenenar, quemar, dormir, paralizar).
        /// </summary>
        public SpecialStatus SpecialStatus { get; set; }

        /// <summary>
        /// Propiedad que indica si el movimiento es un ataque especial.
        /// Un movimiento es considerado un ataque especial si tiene algún estado como Envenenado, Quemado, Dormido o Paralizado.
        /// </summary>
        public bool SpecialAttack =>
            SpecialStatus == SpecialStatus.Poisoned ||
            SpecialStatus == SpecialStatus.Burned ||
            SpecialStatus == SpecialStatus.Asleep ||
            SpecialStatus == SpecialStatus.Paralyzed;

        /// <summary>
        /// Constructor que permite crear un movimiento con un estado especial opcional.
        /// </summary>
        /// <param name="SpecialStatus">Estado especial del movimiento, por defecto es <see cref="SpecialStatus.NoneStatus"/>.</param>
        public Move(SpecialStatus specialStatus = SpecialStatus.NoneStatus)
        {
            SpecialStatus = specialStatus;
        }
    }
}