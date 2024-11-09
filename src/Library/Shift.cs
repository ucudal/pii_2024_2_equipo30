namespace Library;

public class Shift
{
    public Player actualPlayer { get; private set; }
    public Player enemyPlayer { get; private set; }
    public int shiftNumber { get; private set; }
    private int actualShift;

    public Shift(Player jugador1, Player jugador2)
    {
        actualPlayer = jugador1;
        enemyPlayer = jugador2;
        shiftNumber = 1;
    }

    public void SwitchShift()
    {
        var temp = actualPlayer;
        actualPlayer = enemyPlayer;
        enemyPlayer = temp;
        shiftNumber++;
    }

    public void ShowShift()
    {
        Console.WriteLine($"-- Shift {shiftNumber} / {actualPlayer.NamePlayer} es tu turno! --");
    }
    
    // Método para ejecutar un ataque especial con restricción de turnos
    public bool ExecuteSpecialAttack(Player player, Pokemon attacker, Move movements, int actualShift)
    {
        // Verificar si el ataque especial se puede realizar
        if (!player.CanUseEspecialAtack(movements.MoveDetails.Name, actualShift))
        {
            Console.WriteLine($"No puedes usar el ataque especial {movements.MoveDetails.Name} en este momento. Debes esperar más turnos.");
            return false;
        }

        // Realizar el ataque especial
        attacker.AttackP(player, enemyPlayer.actualPokemon, movements, actualShift);
        Console.WriteLine($"{player.NamePlayer} usó el ataque especial {movements.MoveDetails.Name} causando daño!");

        // Registrar el ataque especial solo si el ataque fue exitoso
        player.RegisterSpecialAttack(movements.MoveDetails.Name, actualShift);

        return true;
    }
}
