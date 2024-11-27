using DSharpPlus.SlashCommands;

namespace Library.BotDiscord;

public class BotQueuePlayers : ApplicationCommandModule
{
    private readonly Queue<Player> _players = new Queue<Player>();
    public List<Player> PlayersInGame { get; set; }
    public Battle Battle { get; set; }
    private static BotQueuePlayers instance;
    
    private BotQueuePlayers() {}
    
    public static BotQueuePlayers GetInstance()
    {
        if (instance == null)
        {
            instance = new BotQueuePlayers();
        }
        return instance;
    }

    public string JoinQueue(Player player)
    {
        if (_players.Contains(player))
            return $"{player.NamePlayer}, ya estás en la cola de batalla.";

        _players.Enqueue(player);
        return $"{player.NamePlayer} se ha unido a la cola de batalla.";
    }

    public string ExitQueue(Player player)
    {
        if (!_players.Contains(player))
            return $"{player.NamePlayer}, no estás en la cola.";

        var newQueue = new Queue<Player>(_players);
        _players.Clear();

        foreach (var j in newQueue)
        {
            if (j != player)
                _players.Enqueue(j);
        }
        return $"{player.NamePlayer} ha salido de la cola.";
    }

    public List<Player> ObtenerProximosJugadores()
    {
        if (_players.Count < 2)
            return null;

        return new List<Player> { _players.Dequeue(), _players.Dequeue() };
    }

    public string MostrarJugadores()
    {
        if (_players.Count == 0)
            return "No hay jugadores en la cola.";
        List<string> namelist = new List<string>();
        foreach ( var player in _players)
        {
            namelist.Add(player.NamePlayer);
        }
        return $"Jugadores en cola: {string.Join(", ", namelist)}";
    }
    
    public Player GetPlayerByName(string name, List<Player> players)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre no puede estar vacío o nulo.", nameof(name));

        // Busca el jugador en la cola
        foreach (var player in players)
        {
            if (player.NamePlayer.Equals(name, StringComparison.OrdinalIgnoreCase))
                return player;
        }

        // Si no se encuentra, devuelve null
        return null;
    }
}