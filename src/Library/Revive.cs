namespace Library
{
    /// <summary>
    /// Clase que representa el item "Revivir", un objeto utilizado para revivir a un Pokémon con el 50% de su HP total.
    /// Esta clase hereda de la clase base "Items".
    /// </summary>
    public class Revive : Items
    {
        /// <summary>
        /// Constructor de la clase "Revivir".
        /// Inicializa una nueva instancia de "Revivir" con una cantidad específica.
        /// </summary>
        /// <param name="quantity">La cantidad inicial del objeto "Revivir".</param>
        public Revive(int quantity) : base("Revivir", "Revive a un Pokémon con el 50% de su HP total.", quantity)
        {
        }

        /// <summary>
        /// Método que se utiliza para revivir a un Pokémon fuera de combate.
        /// Revive al Pokémon con el 50% de su vida máxima si el Pokémon está fuera de combate.
        /// </summary>
        /// <param name="pokemon">El Pokémon que se desea revivir.</param>
        public override void Use(Pokemon pokemon)
        {
            // Verifica si hay cantidad disponible del item para ser utilizado.
            if (Quantity > 0)
            {
                // Verifica si el Pokémon está fuera de combate.
                if (pokemon.OutOfAction())
                {
                    // Calcula el 50% del HP máximo del Pokémon.
                    double HpRecovered = pokemon.MaxHealt / 2;

                    // Establece la salud del Pokémon al 50% de su HP máximo.
                    pokemon.Health = HpRecovered;

                    // Marca el Pokémon como en combate nuevamente.
                    pokemon.Outofaction = false;

                    // Asegura que el estado se actualice correctamente.
                    pokemon.OutOfAction();

                    // Mensaje que indica que el Pokémon ha sido revivido y la cantidad de HP recuperada.
                    Console.WriteLine($"El Pokémon {pokemon.Name} ha sido revivido con un {ItemsName} y recuperado {HpRecovered} HP.");

                    // Consume el objeto "Revivir".
                    Consume();
                }
                else
                {
                    // Mensaje indicando que el Pokémon no está fuera de combate y no necesita ser revivido.
                    Console.WriteLine($"\nEl Pokémon {pokemon.Name} no está fuera de combate, no se necesita usar {ItemsName}.\n");
                }
            }
            else
            {
                // Mensaje indicando que no quedan más unidades del objeto "Revivir".
                Console.WriteLine($"\nLa {ItemsName} no se puede usar, no quedan más unidades.\n");
            }
        }
    }
}
