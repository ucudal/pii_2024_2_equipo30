namespace Library
{
    /// <summary>
    /// Interfaz que define las propiedades y métodos básicos para un ítem.
    /// </summary>
    public interface IItem
    {
        /// <summary>
        /// Obtiene o establece la cantidad máxima de salud que puede restaurar el ítem.
        /// </summary>
        int MaxHealt { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del ítem.
        /// </summary>
        string ItemsName { get; set; }

        /// <summary>
        /// Obtiene o establece la descripción del ítem.
        /// </summary>
        string ItemsDescription { get; set; }

        /// <summary>
        /// Obtiene o establece la cantidad de ítems disponibles.
        /// </summary>
        int Quantity { get; set; }

        /// <summary>
        /// Usa el ítem en un Pokémon específico.
        /// </summary>
        /// <param name="pokemon">El Pokémon sobre el cual se va a usar el ítem.</param>
        void Use(Pokemon pokemon);

        /// <summary>
        /// Reduce la cantidad del ítem después de ser consumido.
        /// </summary>
        void Consume();
    }
}