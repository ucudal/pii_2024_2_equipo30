namespace Library;

public class PokemonCreator : IPokemonCreator
    {
        private IPokemonApi pokemonApi;

        public PokemonCreator(IPokemonApi _pokemonApi)
        {
            pokemonApi = _pokemonApi;
        }

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

            // Verificar si existe al menos 4 movimientos y si genericPokemon tiene la información de tipo

            return moves;
        
        }
        private void AssignSpecialStatus(Pokemon pokemon) //Status Especial de ataques
        {
            List<string> Paralize = new List<string>{"electric", "normal", "flying", "ice", "rock", "ground"};
            List<string> Poison = new List<string>{"poison", "bug", "plant", "steel", "grass"};
            List<string> Sleep = new List<string>{"psychic", "fairy", "fighting", "ghost"};
            List<string> Burn = new List<string>{"fire", "dragon", "water"};
            if (pokemon.Moves.Count > 0 && Paralize.Contains(pokemon.Type.TypeDetail.Name))
            {
                pokemon.Moves[pokemon.Moves.Count - 1].EspecialStatus = EspecialStatus.Paralyzed;
            }
            if (pokemon.Moves.Count > 0 && Poison.Contains(pokemon.Type.TypeDetail.Name))
            {
                pokemon.Moves[pokemon.Moves.Count - 1].EspecialStatus = EspecialStatus.Poisoned;
            }
            if (pokemon.Moves.Count > 0 && Sleep.Contains(pokemon.Type.TypeDetail.Name))
            {
                pokemon.Moves[pokemon.Moves.Count - 1].EspecialStatus = EspecialStatus.Asleep;
            }  if (pokemon.Moves.Count > 0 && Burn.Contains(pokemon.Type.TypeDetail.Name))
            {
                pokemon.Moves[pokemon.Moves.Count - 1].EspecialStatus = EspecialStatus.Burned;
            }
            
        }
    }


