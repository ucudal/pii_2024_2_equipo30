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
            if (pokemon.Estado != EstadoEspecial.Ninguno)
            {
                Console.WriteLine($"\n El pokemon {pokemon.Name} estaba {pokemon.Estado}.");
                pokemon.Estado = EstadoEspecial.Ninguno;
                Console.WriteLine($"Ahora el pokemon {pokemon.Name} está completamente curado de todos los efectos especiales.\n");
            }
            else
            {
                Console.WriteLine($"\n El pokemon {pokemon.Name} no tiene efectos de estado activos.\n");
            }
            Consume();
        }
        else
        {
            Console.WriteLine($"\n La {ItemsName} no se puede usar, no quedan más unidades.\n");
        }
    }
}