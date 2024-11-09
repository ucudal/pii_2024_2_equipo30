namespace Library;

/// <summary>
/// Enumeración que representa los distintos estados especiales que un Pokémon puede tener.
/// </summary>
public enum EspecialStatus
{
    /// <summary>
    /// Sin estado especial.
    /// </summary>
    NoneStatus,
    
    /// <summary>
    /// El Pokémon está envenenado.
    /// </summary>
    Poisoned,
    
    /// <summary>
    /// El Pokémon está quemado.
    /// </summary>
    Burned,
    
    /// <summary>
    /// El Pokémon está dormido.
    /// </summary>
    Asleep,
    
    /// <summary>
    /// El Pokémon está paralizado.
    /// </summary>
    Paralyzed
}