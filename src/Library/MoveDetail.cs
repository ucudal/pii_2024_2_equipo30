namespace Library
{
    /// <summary>
    /// Clase que representa los detalles de un movimiento específico que un Pokémon puede realizar.
    /// </summary>
    public class MoveDetail
    {
        /// <summary>
        /// Nombre del movimiento.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Poder del movimiento (daño que puede causar). 
        /// Puede ser nulo si el movimiento no tiene un valor de poder definido.
        /// </summary>
        public int? Power { get; set; }

        /// <summary>
        /// Precisión del movimiento (probabilidad de acierto).
        /// Puede ser nulo si el movimiento no tiene un valor de precisión definido.
        /// </summary>
        public int? Accuracy { get; set; }

        /// <summary>
        /// URL que apunta a información adicional del movimiento, usualmente para obtener detalles de una API.
        /// </summary>
        public string URL { get; set; }
    }
}