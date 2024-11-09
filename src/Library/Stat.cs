using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library
{
    /// <summary>
    /// Clase que representa una estadística de un Pokémon.
    /// Contiene detalles sobre la estadística específica y su valor base.
    /// </summary>
    public class Stat
    {
        /// <summary>
        /// Detalles específicos de la estadística.
        /// Esta propiedad mapea el JSON de la API utilizando <see cref="JsonPropertyNameAttribute"/>.
        /// </summary>
        [JsonPropertyName("stat")]
        public StatsDetail StatsDetail { get; set; }

        /// <summary>
        /// Valor base de la estadística del Pokémon.
        /// Por ejemplo, el valor base de ataque, defensa, etc.
        /// </summary>
        public int base_stat { get; set; }
    }
}