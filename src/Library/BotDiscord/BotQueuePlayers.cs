using DSharpPlus.SlashCommands;

namespace Library.BotDiscord;

public class BotQueuePlayers : ApplicationCommandModule
{
    private readonly Queue<string> _players = new Queue<string>();

    public string JoinQueue(string player)
    {
        if (_players.Contains(player))
            return $"{player}, ya estás en la cola de batalla.";

        _players.Enqueue(player);
        return $"{player} se ha unido a la cola de batalla.";
    }

    public string ExitQueue(string player)
    {
        if (!_players.Contains(player))
            return $"{player}, no estás en la cola.";

        var newQueue = new Queue<string>(_players);
        _players.Clear();

        foreach (var j in newQueue)
        {
            if (j != player)
                _players.Enqueue(j);
        }
        return $"{player} ha salido de la cola.";
    }

    public List<string> ObtenerProximosJugadores()
    {
        if (_players.Count < 2)
            return null;

        return new List<string> { _players.Dequeue(), _players.Dequeue() };
    }

    public string MostrarJugadores()
    {
        if (_players.Count == 0)
            return "No hay jugadores en la cola.";

        return "Jugadores en cola: " + string.Join(", ", _players);
    }
}//Enqueue y Dequeue no se pueden traducir mas a ingles