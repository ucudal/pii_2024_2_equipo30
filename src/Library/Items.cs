namespace Library
{
    /// <summary>
    /// Clase abstracta que implementa la interfaz IItem.
    /// Define los elementos básicos de los ítems que pueden ser usados en la batalla.
    /// </summary>
    public abstract class Items : IItem
    {
        /// <summary>
        /// Propiedad que indica el máximo de salud que puede restaurar el ítem.
        /// </summary>
        public int MaxHealt { get; set; }

        /// <summary>
        /// Propiedad que guarda el nombre del ítem.
        /// </summary>
        public string ItemsName { get; set; }

        /// <summary>
        /// Propiedad que guarda la descripción del ítem.
        /// </summary>
        public string ItemsDescription { get; set; }

        /// <summary>
        /// Propiedad que indica la cantidad de ítems que quedan en el inventario.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Constructor que inicializa un ítem con su nombre, descripción y cantidad.
        /// Este constructor asegura que los ítems se instancien correctamente.
        /// </summary>
        /// <param name="itemsName">Nombre del ítem.</param>
        /// <param name="itemsDescription">Descripción del ítem.</param>
        /// <param name="quantity">Cantidad inicial del ítem.</param>
        public Items(string itemsName, string itemsDescription, int quantity)
        {
            ItemsName = itemsName;
            ItemsDescription = itemsDescription;
            Quantity = quantity;
        }

        /// <summary>
        /// Método abstracto que debe ser implementado por las clases derivadas para aplicar el efecto del ítem sobre un Pokémon.
        /// </summary>
        /// <param name="pokemon">El Pokémon al que se aplicará el ítem.</param>
        public abstract void Use(Pokemon pokemon);
        
        /// <summary>
        /// Método para consumir un ítem, reduciendo su cantidad y notificando al jugador.
        /// Si la cantidad es mayor que 0, se decrementa. Si es 0, se indica que no quedan más ítems.
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
}