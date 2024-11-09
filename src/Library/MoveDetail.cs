namespace Library
{
    /// <summary>
    /// Clase que representa los detalles de un movimiento de un Pokémon.
    /// Contiene información sobre el nombre, poder, precisión y URL del movimiento.
    /// </summary>
    public class MoveDetail
    {
        /// <summary>
        /// Nombre del movimiento del Pokémon.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Poder del movimiento (daño potencial). Puede ser nulo si el movimiento no tiene poder fijo.
        /// </summary>
        public int? Power { get; set; }

        /// <summary>
        /// Precisión del movimiento (probabilidad de éxito). Puede ser nulo si el movimiento no tiene una precisión definida.
        /// </summary>
        public int? Accuracy { get; set; }

        /// <summary>
        /// URL que apunta a más detalles sobre el movimiento, normalmente proporcionada por una API externa.
        /// </summary>
        public string URL { get; set; }
    }
}