namespace Library;

public interface IPokemon
{
    string Name { get; set; }
    double Health { get; set; }
    double VidaMax { get; set; }
    int Id { get; set; }
    int Attack { get; set; }
    int Defense { get; set; }
    int SpecialAttack { get; set; }
    int SpecialDefense { get; set; }
    Type Type { get; set; }
    List<Move> Moves { get; set; }
    EstadoEspecial Estado { get; set; }
    int TurnosRestantesDeSueño { get; set; }

    double CalcularDaño(Move movimiento, double efectividad, Pokemon oponente);
    bool PuedeAtacar();
    void Atacar(Jugador jugador, Pokemon oponente, Move movimiento, int turnoActual);
    void ProcesarEstado(Pokemon oponente = null);
    bool EstaFueraDeCombate();
}