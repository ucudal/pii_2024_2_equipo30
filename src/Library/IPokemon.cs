namespace Library;

public interface IPokemon
{
    string Name { get; set; }
    double Health { get; set; }
    double MaxHealt { get; set; }
    int Id { get; set; }
    int Attack { get; set; }
    int Defense { get; set; }
    int SpecialAttack { get; set; }
    int SpecialDefense { get; set; }
    Type Type { get; set; }
    List<Move> Moves { get; set; }
    EspecialStatus Status { get; set; }
    int SleepTurnsLeft { get; set; }

    double CalculateDamage(Move movimiento, double efectividad, Pokemon oponente);
    bool CanAtack();
    void AttackP(Player player, Pokemon enemy, Move movement, int currentShift);
    void ProcessStatus(Pokemon enemy = null);
    bool OutOfAction();
}