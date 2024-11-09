namespace Library
{
    /// <summary>
    /// Interfaz que define las propiedades y métodos básicos para un ítem en el juego.
    /// Esta interfaz se ha diseñado para seguir la guía de diseño y mantener un bajo acoplamiento entre clases.
    /// </summary>
    public interface IItem
    {
        /// <summary>
        /// Propiedad que indica la cantidad máxima de salud que puede recuperar o modificar el ítem.
        /// </summary>
        int MaxHealt { get; set; }

        /// <summary>
        /// Propiedad que contiene el nombre del ítem.
        /// </summary>
        string ItemsName { get; set; }

        /// <summary>
        /// Propiedad que describe el ítem.
        /// </summary>
        string ItemsDescription { get; set; }

        /// <summary>
        /// Propiedad que indica la cantidad de este ítem disponible.
        /// </summary>
        int Quantity { get; set; }

        /// <summary>
        /// Método para usar el ítem. Los efectos del ítem se aplican sobre el Pokémon pasado como parámetro.
        /// Según la guía de diseño, este método es necesario para que el ítem tenga efecto.
        /// </summary>
        /// <param name="pokemon">El Pokémon sobre el que se aplicará el ítem.</param>
        void Use(Pokemon pokemon);

        /// <summary>
        /// Método para consumir el ítem. Después de usar el ítem, se decrementa su cantidad.
        /// </summary>
        void Consume();
    }
}