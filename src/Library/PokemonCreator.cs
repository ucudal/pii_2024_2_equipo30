namespace Library
{
    /// <summary>
    /// Clase que implementa <see cref="IPokemonCreator"/> y se encarga de crear un objeto Pokémon a partir de la API.
    /// </summary>
    public class PokemonCreator : IPokemonCreator
    {
        /// <summary>
        /// Instancia de <see cref="IPokemonApi"/> utilizada para interactuar con la API de Pokémon.
        /// </summary>
        private IPokemonApi pokemonApi;

        /// <summary>
        /// Constructor que inicializa la clase `PokemonCreator` con una instancia de la API de Pokémon.
        /// </summary>
        /// <param name="_pokemonApi">Instancia de la API de Pokémon para realizar las solicitudes necesarias.</param>
        public PokemonCreator(IPokemonApi _pokemonApi)
        {
            pokemonApi = _pokemonApi;
        }

        /// <summary>
        /// Crea un objeto <see cref="Pokemon"/> con todos sus detalles utilizando la API de Pokémon.
        /// </summary>
        /// <param name="pokemonId">Identificador o nombre del Pokémon que se desea crear.</param>
        /// <returns>Una tarea que representa la operación asíncrona y devuelve un objeto <see cref="Pokemon"/>.</returns>
        public async Task<Pokemon> CreatePokemon(string pokemonId)
        {
            var genericPokemon = await pokemonApi.GetPokemonDetails(pokemonId);
            if (genericPokemon?.Types == null || genericPokemon.Stats == null || genericPokemon.Moves == null)
            {
                return null;
            }

            Type type = new Type();
            type.SetType(genericPokemon.Types[0].TypeDetail.Name);
            List<Move> movementsList = await GetMoves(genericPokemon.Moves, pokemonId);
            
            Pokemon pokemon = new Pokemon(
                genericPokemon.Name,
                genericPokemon.Id,
                genericPokemon.Stats[0].base_stat,  // Vida
                genericPokemon.Stats[1].base_stat,  // Ataque
                genericPokemon.Stats[2].base_stat,  // Defensa
                genericPokemon.Stats[3].base_stat,  // Ataque Especial
                genericPokemon.Stats[4].base_stat,  // Defensa Especial
                type,
                movementsList
            );
            AssignSpecialStatus(pokemon);
            return pokemon;
        }

        /// <summary>
        /// Obtiene la lista de movimientos para un Pokémon específico utilizando la API.
        /// </summary>
        /// <param name="listmoves">Lista de movimientos genéricos del Pokémon.</param>
        /// <param name="pokemonId">Identificador o nombre del Pokémon.</param>
        /// <returns>Una tarea que representa la operación asíncrona y devuelve una lista de <see cref="Move"/>.</returns>
        private async Task<List<Move>> GetMoves(List<Move> listmoves, string pokemonId)
        {
            var genericPokemon = await pokemonApi.GetPokemonDetails(pokemonId);
            var moves = new List<Move>();
            int counter = 0;

            foreach (var move in listmoves)
            {
                if (counter < 4)
                {
                    var moveDetail = await pokemonApi.GetMoveDetails(move.MoveDetails.URL);
                    if (moveDetail?.Accuracy != null && moveDetail.Power != null)
                    {
                        moves.Add(new Move
                        {
                            MoveDetails = new MoveDetail
                            {
                                Name = moveDetail.Name,
                                Accuracy = moveDetail.Accuracy,
                                Power = moveDetail.Power
                            }
                        });
                    }
                    counter++;
                }
            }

            return moves;
        }

        /// <summary>
        /// Asigna un estado especial a los movimientos del Pokémon en función de su tipo.
        /// </summary>
        /// <param name="pokemon">El Pokémon al cual se le asignarán los estados especiales a sus movimientos.</param>
        private void AssignSpecialStatus(Pokemon pokemon)
        {
            List<string> Paralize = new List<string> { "electric", "normal", "flying", "ice", "rock", "ground" };
            List<string> Poison = new List<string> { "poison", "bug", "plant", "steel", "grass" };
            List<string> Sleep = new List<string> { "psychic", "fairy", "fighting", "ghost" };
            List<string> Burn = new List<string> { "fire", "dragon", "water" };

            if (pokemon.Moves.Count > 0)
            {
                if (Paralize.Contains(pokemon.Type.TypeDetail.Name))
                {
                    pokemon.Moves[pokemon.Moves.Count - 1].EspecialStatus = EspecialStatus.Paralyzed;
                }
                if (Poison.Contains(pokemon.Type.TypeDetail.Name))
                {
                    pokemon.Moves[pokemon.Moves.Count - 1].EspecialStatus = EspecialStatus.Poisoned;
                }
                if (Sleep.Contains(pokemon.Type.TypeDetail.Name))
                {
                    pokemon.Moves[pokemon.Moves.Count - 1].EspecialStatus = EspecialStatus.Asleep;
                }
                if (Burn.Contains(pokemon.Type.TypeDetail.Name))
                {
                    pokemon.Moves[pokemon.Moves.Count - 1].EspecialStatus = EspecialStatus.Burned;
                }
            }
        }
    }
}
