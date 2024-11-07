namespace Library;

public class PokemonData
{
        public string Name { get; set; }
        public double Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public List<Move> Moves { get; set; }

        public PokemonData()
        {
        }
}