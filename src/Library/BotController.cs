namespace Library;
using Discord;
using Discord.WebSocket;


public class BotController
{
    private BattleSystem _battleSystem;
    private Player _player1;  // El jugador humano
    private Player _player2;  // El bot
    private static Random random = new Random();


    public BotController(BattleSystem battleSystem, Player player1, Player player2)
    {
        _battleSystem = battleSystem;
        _player1 = player1;
        _player2 = player2;
    }

    public async Task ProcessCommand(string command, IMessageChannel channel)
    {
        string[] parts = command.Split(' ');
        switch (parts[0].ToLower())
        {
            case "attack":
                int moveIndex = int.Parse(parts[1]);
                // Ejecuta el ataque
                _battleSystem.ExecuteAttack(moveIndex);
                await channel.SendMessageAsync($"¡Has usado el movimiento {moveIndex}!");
                break;
            case "switch":
                int pokemonIndex = int.Parse(parts[1]);
                // Cambia de Pokémon
                _player1.SwitchPokemon(pokemonIndex);
                await channel.SendMessageAsync($"¡Cambiado a Pokémon {pokemonIndex}!");
                break;
            default:
                await channel.SendMessageAsync("Comando no reconocido. Usa `attack [número]` o `switch [número]`.");
                break;
        }
        // Turno del bot
        BotTurn(channel);
    }
    private async Task BotTurn(IMessageChannel channel)
    {
        // Lógica para que el bot elija movimiento o cambie de Pokémon
        var botPokemon = _player2.actualPokemon;  // El Pokémon actual del bot
        var availableMoves = botPokemon.Moves;  // Movimientos disponibles del Pokémon del bot

        // Si el Pokémon del bot está debilitado, cambiar de Pokémon
        if (botPokemon.OutOfAction())
        {
            var faintedPokemon = _player2.Team.FirstOrDefault(pokemon => pokemon.OutOfAction()); // Llama al método aquí también
            if (faintedPokemon != null)
            {
                int pokemonIndex = _player2.Team.IndexOf(faintedPokemon);
                _player2.SwitchPokemon(pokemonIndex); // Cambiar al siguiente Pokémon disponible
                await channel.SendMessageAsync($"¡El bot cambia a {botPokemon.Name}!");
            }
        }
        else
        {
            // Si el bot tiene ataques disponibles, elige uno aleatoriamente
            if (availableMoves.Any())
            {
                var randomMove = availableMoves[random.Next(availableMoves.Count)];  // Selecciona un movimiento aleatorio
                //await channel.SendMessageAsync($"¡El bot usa {randomMove.Name}!");
                int moveIndex = availableMoves.IndexOf(randomMove);  // Obtén el índice del movimiento
                _battleSystem.ExecuteAttack(moveIndex);  // Ejecuta el ataque utilizando el índice
            }
        }

        // Aquí puedes agregar más lógica si el bot quiere hacer algo adicional
        await channel.SendMessageAsync("Turno del bot finalizado.");
    }
}
