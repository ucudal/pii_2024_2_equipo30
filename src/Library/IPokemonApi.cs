using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library
{
    /// <summary>
    /// Interfaz que define los métodos para interactuar con la API de Pokémon y obtener detalles de Pokémon y movimientos.
    /// </summary>
    public interface IPokemonApi
    {
        /// <summary>
        /// Obtiene los detalles de un Pokémon a partir de su identificador utilizando la API de Pokémon.
        /// </summary>
        /// <param name="pokemonId">El identificador del Pokémon (nombre o número).</param>
        /// <returns>Devuelve un objeto <see cref="Pokemon"/> con los detalles del Pokémon solicitado.</returns>
        Task<Pokemon> GetPokemonDetails(string pokemonId);

        /// <summary>
        /// Obtiene los detalles de un movimiento específico a partir de una URL proporcionada por la API de Pokémon.
        /// </summary>
        /// <param name="url">La URL que apunta al recurso del movimiento específico en la API.</param>
        /// <returns>Devuelve un objeto <see cref="MoveDetail"/> con los detalles del movimiento solicitado.</returns>
        Task<MoveDetail> GetMoveDetails(string url);
    }
}