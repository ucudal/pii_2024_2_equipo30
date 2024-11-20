namespace Library
{
    /// <summary>
    /// Clase que representa una SuperPoción, que cura una gran cantidad de puntos de vida (70 HP según la guía del usuario).
    /// Esta clase hereda de <see cref="Items"/>.
    /// </summary>
    public class SuperPotion : Items
    {
        /// <summary>
        /// Cantidad de puntos de vida que la SuperPoción puede recuperar.
        /// </summary>
        private int HpRecovered;

        /// <summary>
        /// Constructor para inicializar una SuperPoción con la cantidad disponible y la cantidad de salud a recuperar.
        /// </summary>
        /// <param name="quantity">Cantidad de SuperPociones disponibles.</param>
        /// <param name="hpRecovered">Cantidad de puntos de vida que recupera cada SuperPoción.</param>
        public SuperPotion(int quantity, int hpRecovered) : base("SuperPoción", "Poción mejorada, puede curar más que una poción normal", quantity)
        {
            HpRecovered = hpRecovered;
        }

        /// <summary>
        /// Método para usar la SuperPoción en un Pokémon específico.
        /// Recupera la salud del Pokémon hasta un máximo igual a su salud máxima (HP).
        /// </summary>
        /// <param name="pokemon">El Pokémon al cual se aplicará la SuperPoción.</param>
        public override void Use(Pokemon pokemon)
        {
            double newHealth = pokemon.Health + HpRecovered;

            // Asegurarse de no exceder la salud máxima del Pokémon
            if (newHealth > pokemon.MaxHealt)
            {
                pokemon.Health = pokemon.MaxHealt;
            }
            else
            {
                pokemon.Health = newHealth;
            }

            // Consumir una SuperPoción
            Consume();

            Console.WriteLine($"\n El Pokémon {pokemon.Name} ha recuperado {HpRecovered} puntos de salud. Ahora tiene {pokemon.Health:F1}/{pokemon.MaxHealt} puntos de vida.\n");
        }
    }
}