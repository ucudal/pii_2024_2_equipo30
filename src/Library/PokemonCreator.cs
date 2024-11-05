namespace Library;

public class PokemonCreator : IPokemonCreator
    {
        private readonly IPokemonApi pokemonApi;

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
                

            Type tipo = new Library.Type();
            tipo.SetType(genericPokemon.Types[0].TypeDetail.Name);
            List<Move> listaMovimientos = await GetMoves(genericPokemon.Moves);

            return new Pokemon(
                genericPokemon.Name,
                genericPokemon.Id,
                genericPokemon.Stats[0].base_stat,  // Vida
                genericPokemon.Stats[1].base_stat,  // Ataque
                genericPokemon.Stats[2].base_stat,  // Defensa
                genericPokemon.Stats[3].base_stat,  // Ataque Especial
                genericPokemon.Stats[4].base_stat,  // Defensa Especial
                tipo,
                listaMovimientos
            );
        }

        private async Task<List<Move>> GetMoves(List<Move> moveInfos)
        {
            var moves = new List<Move>();
            int counter = 0;
            foreach (var moveInfo in moveInfos)
            {
                if (counter >= 10) break;
                var moveDetail = await pokemonApi.GetMoveDetails(moveInfo.MoveDetails.URL);
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
                    counter++;
                }
            }
            return moves;
        }
    }

