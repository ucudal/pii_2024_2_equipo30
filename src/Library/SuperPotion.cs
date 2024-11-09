namespace Library
{
    /// <summary>
    /// Representa una Super Poción que puede curar una gran cantidad de vida a un Pokémon.
    /// De acuerdo con la guía de usuario, esta poción debería restaurar hasta 70 HP.
    /// </summary>
    public class SuperPotion : Items
    {
        /// <summary>
        /// Cantidad de HP que la Super Poción puede recuperar.
        /// </summary>
        private int HpRecovered;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="SuperPotion"/> con una cantidad específica y una cantidad de HP que recupera.
        /// </summary>
        /// <param name="quantity">Cantidad de Super Poción disponible.</param>
        /// <param name="hpRecovered">Cantidad de HP que se recupera con cada uso.</param>
        public SuperPotion(int quantity, int hpRecovered) : base("SuperPoción", "Poción mejorada, puede curar más que una poción normal", quantity)
        {
            HpRecovered = hpRecovered;
        }

        /// <summary>
        /// Usa la Super Poción en un Pokémon específico, restaurando una cantidad de HP definida.
        /// Si el HP recuperado excede el HP máximo del Pokémon, se ajusta al máximo permitido.
        /// </summary>
        /// <param name="pokemon">El Pokémon al cual se le aplicará la Super Poción.</param>
        public override void Use(Pokemon pokemon)
        {
            double newHealth = pokemon.Health + HpRecovered;
            
            // Asegura que el HP no exceda el máximo
            if (newHealth > pokemon.MaxHealt)
            {
                pokemon.Health = pokemon.MaxHealt;
            }
            else
            {
                pokemon.Health = newHealth;
            }

            // Reduce la cantidad de Super Pociones en el inventario y muestra el estado de salud del Pokémon
            Consume();
            Console.WriteLine($"\n El Pokémon {pokemon.Name} ha recuperado {HpRecovered} puntos de salud. Ahora tiene {pokemon.Health:F1}/{pokemon.MaxHealt} puntos de vida.\n");
        }
    }
}
