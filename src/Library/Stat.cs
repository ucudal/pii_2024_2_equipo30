using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace Library
{
    /// <summary>
    /// Representa una estadística de un Pokémon o personaje, con un detalle específico y un valor base.
    /// </summary>
    public class Stat
    {
        /// <summary>
        /// Detalle de la estadística, incluye el nombre específico de la estadística.
        /// </summary>
        [JsonPropertyName("stat")]
        public StatsDetail StatsDetail { get; set; }

        /// <summary>
        /// Valor base de la estadística.
        /// </summary>
        public int base_stat { get; set; }
    }
}