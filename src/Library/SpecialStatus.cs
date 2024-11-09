namespace Library
{
    /// <summary>
    /// Enumera los posibles estados especiales que un Pokémon o personaje puede tener.
    /// </summary>
    public enum SpecialStatus
    {
        /// <summary>
        /// No tiene ningún estado especial.
        /// </summary>
        NoneStatus,

        /// <summary>
        /// Indica que el Pokémon está envenenado.
        /// </summary>
        Poisoned,

        /// <summary>
        /// Indica que el Pokémon está quemado.
        /// </summary>
        Burned,

        /// <summary>
        /// Indica que el Pokémon está dormido.
        /// </summary>
        Asleep,

        /// <summary>
        /// Indica que el Pokémon está paralizado.
        /// </summary>
        Paralyzed
    }
}