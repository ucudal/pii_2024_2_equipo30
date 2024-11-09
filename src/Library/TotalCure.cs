namespace Library
{
    /// <summary>
    /// Clase que representa el ítem "Cura Total", el cual cura a un Pokémon de efectos de ataques especiales (dormido, paralizado, envenenado, o quemado).
    /// Esta clase hereda de <see cref="Items"/>.
    /// </summary>
    public class TotalCure : Items
    {
        /// <summary>
        /// Constructor para inicializar la "Cura Total" con la cantidad disponible.
        /// </summary>
        /// <param name="quantity">Cantidad de ítems "Cura Total" disponibles.</param>
        public TotalCure(int quantity) : base("Cura Total", "Cura a un Pokémon de efectos de ataques especiales (dormido, paralizado, envenenado, o quemado).", quantity)
        {

        }

        /// <summary>
        /// Método para usar el ítem "Cura Total" en un Pokémon.
        /// Cura los efectos de ataques especiales, incluyendo envenenamiento, parálisis, y quemaduras.
        /// </summary>
        /// <param name="pokemon">El Pokémon al cual se aplicará la "Cura Total".</param>
        public override void Use(Pokemon pokemon)
        {
            if (Quantity > 0)
            {
                if (pokemon.Status == EspecialStatus.Poisoned)
                {
                    pokemon.Status = EspecialStatus.NoneStatus;
                    Console.WriteLine($"El Pokémon {pokemon.Name} ya no está envenenado.");
                }
                if (pokemon.Status == EspecialStatus.Paralyzed)
                {
                    pokemon.Status = EspecialStatus.NoneStatus;
                    Console.WriteLine($"El Pokémon {pokemon.Name} ya no está paralizado.");
                }
                if (pokemon.Status == EspecialStatus.Burned)
                {
                    pokemon.Status = EspecialStatus.NoneStatus;
                    Console.WriteLine($"El Pokémon {pokemon.Name} ya no está quemado.");
                }

                // Consumir una unidad de Cura Total
                Consume();
            }
            else
            {
                Console.WriteLine($"La cura {ItemsName} no se puede usar, no quedan más unidades disponibles.");
            }
        }
    }
}
