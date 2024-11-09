namespace Library
{
    /// <summary>
    /// Clase responsable de crear instancias de Pokémon a partir de la información proporcionada por la API de Pokémon.
    /// Implementa la interfaz "IPokemonCreator".
    /// </summary>
    public class PokemonCreator : IPokemonCreator
    {
        /// <summary>
        /// Instancia de la API de Pokémon que se utiliza para obtener los detalles de un Pokémon.
        /// </summary>
        private IPokemonApi pokemonApi;

        /// <summary>
        /// Constructor de la clase "PokemonCreator".
        /// Inicializa una nueva instancia de "PokemonCreator" con una instancia de la API de Pokémon.
        /// </summary>
        /// <param name="_pokemonApi">La API de Pokémon utilizada para obtener los datos de un Pokémon.</param>
        public PokemonCreator(IPokemonApi _pokemonApi)
        {
            pokemonApi = _pokemonApi;
        }

        /// <summary>
        /// Método asíncrono que crea un Pokémon basado en su identificador.
        /// Obtiene los detalles del Pokémon y crea una nueva instancia del objeto "Pokemon".
        /// </summary>
        /// <param name="pokemonId">El identificador del Pokémon (nombre o ID) utilizado para crear el objeto Pokémon.</param>
        /// <returns>Devuelve una tarea que se resuelve en un objeto Pokémon creado.</returns>
        public async Task<Pokemon> CreatePokemon(string pokemonId)
        {
            var genericPokemon = await pokemonApi.GetPokemonDetails(pokemonId);
            if (genericPokemon?.Types == null || genericPokemon.Stats == null || genericPokemon.Moves == null)
            {
                return null; // Retorna nulo si los datos del Pokémon no son suficientes.
            }

            // Crear una instancia del tipo del Pokémon a partir de su tipo principal.
            Type type = new Type();
            type.SetType(genericPokemon.Types[0].TypeDetail.Name);

            // Obtener los movimientos del Pokémon (máximo 4 movimientos).
            List<Move> movementsList = await GetMoves(genericPokemon.Moves, pokemonId);

            // Crear una nueva instancia del Pokémon con los datos obtenidos.
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

            // Asignar un estado especial al Pokémon, según su tipo.
            AssignSpecialStatus(pokemon);
            return pokemon;
        }

        /// <summary>
        /// Método privado que obtiene una lista de movimientos del Pokémon a partir de la API.
        /// Se limita a obtener un máximo de 4 movimientos.
        /// </summary>
        /// <param name="listmoves">Lista de movimientos obtenida del objeto genérico del Pokémon.</param>
        /// <param name="pokemonId">El identificador del Pokémon para obtener los detalles específicos de sus movimientos.</param>
        /// <returns>Devuelve una tarea que se resuelve en una lista de movimientos.</returns>
        private async Task<List<Move>> GetMoves(List<Move> listmoves, string pokemonId)
        {
            var genericPokemon = await pokemonApi.GetPokemonDetails(pokemonId);
            var moves = new List<Move>();
            int counter = 0;

            // Recorre los movimientos disponibles y selecciona hasta 4 movimientos.
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
        /// Método privado que asigna un estado especial al Pokémon dependiendo de su tipo.
        /// El último movimiento del Pokémon se actualizará con un estado especial (Parálisis, Veneno, Sueño, o Quemadura).
        /// </summary>
        /// <param name="pokemon">El objeto Pokémon al cual se le asignará el estado especial.</param>
        private void AssignSpecialStatus(Pokemon pokemon)
        {
            // Listas de tipos que pueden recibir un estado especial.
            List<string> Paralize = new List<string> { "electric", "normal", "flying", "ice", "rock", "ground" };
            List<string> Poison = new List<string> { "poison", "bug", "plant", "steel", "grass" };
            List<string> Sleep = new List<string> { "psychic", "fairy", "fighting", "ghost" };
            List<string> Burn = new List<string> { "fire", "dragon", "water" };

            // Asignar el estado especial basado en el tipo del Pokémon.
            if (pokemon.Moves.Count > 0 && Paralize.Contains(pokemon.Type.TypeDetail.Name))
            {
                pokemon.Moves[pokemon.Moves.Count - 1].SpecialStatus = SpecialStatus.Paralyzed;
            }
            if (pokemon.Moves.Count > 0 && Poison.Contains(pokemon.Type.TypeDetail.Name))
            {
                pokemon.Moves[pokemon.Moves.Count - 1].SpecialStatus = SpecialStatus.Poisoned;
            }
            if (pokemon.Moves.Count > 0 && Sleep.Contains(pokemon.Type.TypeDetail.Name))
            {
                pokemon.Moves[pokemon.Moves.Count - 1].SpecialStatus = SpecialStatus.Asleep;
            }
            if (pokemon.Moves.Count > 0 && Burn.Contains(pokemon.Type.TypeDetail.Name))
            {
                pokemon.Moves[pokemon.Moves.Count - 1].SpecialStatus = SpecialStatus.Burned;
            }
        }
    }
}
