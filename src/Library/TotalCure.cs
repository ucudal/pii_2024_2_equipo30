namespace Library
{
    /// <summary>
    /// Representa un objeto Cura Total, que cura a un Pokémon de efectos de ataques especiales.
    /// Elimina estados como envenenado, paralizado o quemado.
    /// </summary>
    /// <remarks>
    /// La cura no afecta al estado "Dormido".
    /// </remarks>
    public class TotalCure : Items
    {
        /// <summary>
        /// Inicializa una nueva instancia de <see cref="TotalCure"/> con una cantidad específica.
        /// </summary>
        /// <param name="quantity">Cantidad de objetos de Cura Total.</param>
        public TotalCure(int quantity) : base("Cura Total", "Cura a un Pokémon de efectos de ataques especiales (dormido, paralizado, envenenado, o quemado).", quantity)
        {
        }

        /// <summary>
        /// Usa el objeto en un Pokémon específico, eliminando los efectos de estados como envenenado, paralizado o quemado, si el objeto tiene cantidad disponible.
        /// </summary>
        /// <param name="pokemon">El Pokémon en el cual se usará la Cura Total.</param>
        public override void Use(Pokemon pokemon)
        {
            if (Quantity > 0)
            {
                if (pokemon.Status == SpecialStatus.Poisoned)
                {
                    pokemon.Status = SpecialStatus.NoneStatus;
                    Console.WriteLine($"El Pokémon {pokemon.Name} ya no está envenenado.");
                }
                if (pokemon.Status == SpecialStatus.Paralyzed)
                {
                    pokemon.Status = SpecialStatus.NoneStatus;
                    Console.WriteLine($"El Pokémon {pokemon.Name} ya no está paralizado.");
                }
                if (pokemon.Status == SpecialStatus.Burned)
                {
                    pokemon.Status = SpecialStatus.NoneStatus;
                    Console.WriteLine($"El Pokémon {pokemon.Name} ya no está quemado.");
                }
                Consume();
            }
            else
            {
                Console.WriteLine($"La cura {ItemsName} no se puede usar, no hay más.");
            }
        }
    }
}
