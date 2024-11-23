namespace Library;
using Discord;
using Discord.WebSocket;

public class BattleSystem
{
    private Player player1;
    private Player player2;
    private int currentTurn = 1;
    private static Random random = new Random();
    private BotController botController;
    
    public BattleSystem(Player player1, Player player2)
    {
        this.player1 = player1;
        this.player2 = player2;
        this.botController = new BotController(this, player1, player2);
    }

    public void ExecuteAttack(int moveIndex)
    {
        var attacker = player1.actualPokemon;
        var defender = player2.actualPokemon;
        var move = attacker.Moves[moveIndex];

        if (!attacker.CanAtack())
        {
            Console.WriteLine($"{attacker.Name} no puede atacar debido a su estado.");
            return;
        }

        attacker.AttackP(player1, defender, move, currentTurn);
        currentTurn++;
    }
    
    public void StartBattle(ISocketMessageChannel channel)
    {
        while (!player1.AllOutOfCombat() && !player2.AllOutOfCombat())
        {
            Console.WriteLine("Es tu turno. ¿Qué deseas hacer?");
            string command = Console.ReadLine();

            // Pasamos el canal real a ProcessCommand
            botController.ProcessCommand(command, channel);

            if (player2.AllOutOfCombat()) break;

            Console.WriteLine("Turno del oponente...");
            ExecuteBotTurn();
        }

        Console.WriteLine(player1.AllOutOfCombat() ? "¡Perdiste!" : "¡Ganaste!");
    }






    public void ExecuteBotTurn()
    {
        var enemyPokemon = player2.actualPokemon;
        var availableMoves = enemyPokemon.Moves;

        // Seleccionar movimiento al azar
        var randomMove = availableMoves[random.Next(availableMoves.Count)];
        enemyPokemon.AttackP(player2, player1.actualPokemon, randomMove, currentTurn);
        currentTurn++;
    }
    
}
