using Library;

/// <summary>
/// Clase abstracta que representa un ítem en el juego. Implementa la interfaz <see cref="IItem"/>.
/// </summary>
public abstract class Items : IItem
{
    /// <summary>
    /// Máxima cantidad de salud que el ítem puede restaurar.
    /// </summary>
    public int MaxHealt { get; set; }

    /// <summary>
    /// Nombre del ítem.
    /// </summary>
    public string ItemsName { get; set; }

    /// <summary>
    /// Descripción del ítem.
    /// </summary>
    public string ItemsDescription { get; set; }

    /// <summary>
    /// Cantidad de ítems disponibles para el jugador.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Constructor que inicializa un ítem con su nombre, descripción y cantidad.
    /// Creado para cumplir con el patrón Creator y asignar la responsabilidad de la creación de ítems.
    /// </summary>
    /// <param name="itemsName">Nombre del ítem.</param>
    /// <param name="itemsDescription">Descripción del ítem.</param>
    /// <param name="quantity">Cantidad de ítems disponibles.</param>
    public Items(string itemsName, string itemsDescription, int quantity)
    {
        ItemsName = itemsName;
        ItemsDescription = itemsDescription;
        Quantity = quantity;
    }

    /// <summary>
    /// Método abstracto para usar un ítem en un Pokémon específico.
    /// Debe ser implementado por las clases derivadas para definir el comportamiento específico del ítem.
    /// </summary>
    /// <param name="pokemon">El Pokémon en el cual se usará el ítem.</param>
    public abstract void Use(Pokemon pokemon);

    /// <summary>
    /// Método para reducir la cantidad de ítems disponibles. Indica al jugador cuántos ítems le quedan.
    /// </summary>
    public void Consume()
    {
        if (Quantity > 0)
        {
            Quantity--;
            Console.WriteLine($"\n {ItemsName} ha sido utilizado. Te quedan {Quantity} restantes.\n");
        }
        else
        {
            Console.WriteLine($"\n Ya no te quedan más {ItemsName}.\n");
        }
    }
}
