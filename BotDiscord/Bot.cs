namespace Library;
using Library;
using API;
using DSharpPlus;
using DSharpPlus.SlashCommands;

public class Bot
{
    private DiscordClient _client;
    private SlashCommandsExtension _slashCommands;

    public async Task RunAsync()
    {
        var config = new DiscordConfiguration
        {
            Token = "MTMwNDIwMTYwMjE4MTIzNDczOQ.Gi_jKI.Pa8pvwJsaGVAxR6mzdm41C1EGN0Nrn1uDNpNn4", // Tu token del bot
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.AllUnprivileged
        };

        _client = new DiscordClient(config);

        // Configurar comandos slash
        _slashCommands = _client.UseSlashCommands();
        _slashCommands.RegisterCommands<PokemonCommands>();

        await _client.ConnectAsync();
        await Task.Delay(-1);
    }
}