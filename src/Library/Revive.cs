namespace Library
{
    /// <summary>
    /// Clase que representa el ítem Revivir, el cual se utiliza para revivir a un Pokémon con el 50% de su salud total.
    /// Esta clase hereda de <see cref="Items"/>.
    /// </summary>
    public class Revive : Items
    {
        /// <summary>
        /// Constructor para inicializar el ítem Revive con una cantidad específica.
        /// </summary>
        /// <param name="quantity">Cantidad de ítems Revive disponibles.</param>
        public Revive(int quantity) : base("Revivir", "Revive a un Pokémon con el 50% de su HP total.", quantity)
        {
        }

        /// <summary>
        /// Método para usar el ítem Revive en un Pokémon. Solo se puede usar si el Pokémon está fuera de combate.
        /// </summary>
        /// <param name="pokemon">El Pokémon al cual se aplicará el ítem para revivirlo.</param>
        public override void Use(Pokemon pokemon)
        {
            if (Quantity > 0)
            {
                if (pokemon.OutOfAction()) // Verifica si el Pokémon está fuera de combate
                {
                    double HpRecovered = pokemon.MaxHealt / 2; // Calcula el 50% de la vida máxima del Pokémon

                    pokemon.Health = HpRecovered; // Recupera su salud
                    pokemon.Outofaction = false; // Asegura que el Pokémon esté marcado como en combate

                    // Llama a OutOfAction para asegurar que el estado se actualice correctamente
                    pokemon.OutOfAction();

                    Console.WriteLine($"El Pokémon {pokemon.Name} ha sido revivido con un {ItemsName} y ha recuperado {HpRecovered} HP.");
                    Consume(); // Consume el objeto Revive
                }
                else
                {
                    Console.WriteLine($"\nEl Pokémon {pokemon.Name} no está fuera de combate, no se necesita usar {ItemsName}.\n");
                }
            }
            else
            {
                Console.WriteLine($"\nLa {ItemsName} no se puede usar, no quedan más unidades.\n");
            }
        }
    }
}
