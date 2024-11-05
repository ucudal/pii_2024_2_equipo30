using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library;
public interface IPokemonApi
{
    Task<Pokemon> GetPokemonDetails(string pokemonId);
    Task<MoveDetail> GetMoveDetails(string url);
}