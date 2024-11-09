using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library
{
    /// <summary>
    /// Interfaz que define los métodos esenciales para interactuar con la API de Pokémon.
    /// Permite obtener detalles sobre un Pokémon y sobre sus movimientos a través de la API.
    /// </summary>
    public interface IPokemonApi
    {
        /// <summary>
        /// Método asíncrono para obtener los detalles de un Pokémon basado en su identificador.
        /// </summary>
        /// <param name="pokemonId">El identificador del Pokémon (por ejemplo, su nombre o ID en la API).</param>
        /// <returns>Devuelve una tarea que se resuelve en un objeto Pokémon con sus detalles.</returns>
        Task<Pokemon> GetPokemonDetails(string pokemonId);

        /// <summary>
        /// Método asíncrono para obtener los detalles de un movimiento a partir de la URL proporcionada.
        /// </summary>
        /// <param name="url">La URL que apunta a los detalles de un movimiento en la API.</param>
        /// <returns>Devuelve una tarea que se resuelve en un objeto que contiene los detalles del movimiento.</returns>
        Task<MoveDetail> GetMoveDetails(string url);
    }
}