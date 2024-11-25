using DSharpPlus.SlashCommands;

namespace Library.BotDiscord;

public class BotCommands : ApplicationCommandModule
{
    [SlashCommand("Ping", "Estado del bot")]
    public async Task PingCommand(InteractionContext ctx)
    {
        await ctx.CreateResponseAsync($"Pong! Latencia: {ctx.Client.Ping}");
        
    }

    [SlashCommand("Hello", "Saludo")]
    public async Task HelloCommand(InteractionContext ctx)
    {
        await ctx.CreateResponseAsync($"Hola {ctx.User.Username}!");
    }
    //[SlashCommand("startbattle", "Comienza la batalla")]
    public async Task StartBattleCommand(InteractionContext ctx, string player1Name, string player2Name)
    {
        // Buscar jugadores existentes o crear nuevos
        var player1 = PlayerManager.GetPlayerByName(player1Name) ?? new Player(player1Name);
        var player2 = PlayerManager.GetPlayerByName(player2Name) ?? new Player(player2Name);

        // Instanciar la batalla
        var battle = new Battle(player1, player2);

        // Iniciar la batalla
        await battle.StartBattle(ctx);
    }
    public static class PlayerManager
    {
        private static List<Player> players = new List<Player>();

        public static Player GetPlayerByName(string name)
        {
            return players.FirstOrDefault(p => p.NamePlayer == name);
        }

        public static void AddPlayer(Player player)
        {
            if (!players.Any(p => p.NamePlayer == player.NamePlayer))
            {
                players.Add(player);
            }
        }
    }
    public class Shift
    {
        public Player actualPlayer { get; private set; }
        public Player enemyPlayer { get; private set; }

        public Shift(Player player1, Player player2)
        {
            this.actualPlayer = player1;
            this.enemyPlayer = player2;
        }

        public void SwitchShift()
        {
            var temp = actualPlayer;
            actualPlayer = enemyPlayer;
            enemyPlayer = temp;
        }

        public void ShowShift()
        {
            Console.WriteLine($"Es el turno de {actualPlayer.NamePlayer}");
        }
    }



}