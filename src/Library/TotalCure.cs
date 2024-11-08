namespace Library;
//cura total: Cura a un Pokémon de efectos de ataques especiales(dormido, paralizado, envenenado, o quemado.)
//clase que hereda de items
public class TotalCure : Items
{
    public TotalCure(int quantity) : base("Cura Total", "Cura a un Pokémon de efectos de ataques especiales (dormido, paralizado, envenenado, o quemado).", quantity)
    {

    }

    public override void Use(Pokemon pokemon)
    {
        if (Quantity > 0)
        {
            if (pokemon.Estado == EstadoEspecial.Envenenado)
            {
                pokemon.Estado = EstadoEspecial.Ninguno;
                Console.WriteLine($"El pokemon {pokemon.Name} ya no está envenenado.");
            }
            if (pokemon.Estado == EstadoEspecial.Paralizado)
            {
                pokemon.Estado = EstadoEspecial.Ninguno;
                Console.WriteLine($"El pokemon {pokemon.Name} ya no está paralizado.");
            }
            if (pokemon.Estado == EstadoEspecial.Quemado)
            {
                pokemon.Estado = EstadoEspecial.Ninguno;
                Console.WriteLine($"El pokemon {pokemon.Name} ya no está quemado.");
            }
            Consume();
        }
        else
        {
            Console.WriteLine($"La cura {ItemsName} no se puede usar, no hay mas");
        }
    }
}
