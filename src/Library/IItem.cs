namespace Library;

/// <summary>
/// Interfaz que define la estructura de un ítem en el juego, 
/// siguiendo una guía de diseño orientada a mantener bajo acoplamiento.
/// </summary>
public interface IItem
{
    /// <summary>
    /// Máxima cantidad de salud que el ítem puede restaurar.
    /// </summary>
    int MaxHealt { get; set; }

    /// <summary>
    /// Nombre del ítem.
    /// </summary>
    string ItemsName { get; set; }

    /// <summary>
    /// Descripción del ítem.
    /// </summary>
    string ItemsDescription { get; set; }

    /// <summary>
    /// Cantidad de ítems disponibles para el jugador.
    /// </summary>
    int Quantity { get; set; }

    /// <summary>
    /// Método para usar el ítem en un Pokémon específico.
    /// Este método es necesario para aplicar el efecto del ítem en el Pokémon.
    /// </summary>
    /// <param name="pokemon">El Pokémon en el que se aplicará el ítem.</param>
    void Use(Pokemon pokemon); 

    /// <summary>
    /// Método para consumir el ítem, disminuyendo su cantidad disponible.
    /// </summary>
    void Consume();
}