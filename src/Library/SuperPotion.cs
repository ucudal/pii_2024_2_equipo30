namespace Library;

//pocion que cura gran cantidad de vida (según la guia de usuario tendría que ser 70 HP)
//clase que hereda de items
public class SuperPotion : Items
{
    private int HpRecovered;

    public SuperPotion(int quantity, int hpRecovered) : base("SuperPoción","Poción mejorada, puede curar más que una poción normal", quantity)
    {
        HpRecovered = hpRecovered;
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
