namespace Library;


//Revivir: Revive a un Pokémon con el 50% de su HP total.
//clase que hereda de items
public class Revive : Items
{
    private int HpRecovered;
    
    public Revive(int quantity) : base("Revivir", "Revive a un Pokémon con el 50% de su HP total.", quantity)
    {
        HpRecovered = VidaMax / 2;
    }
    
    public override void Use(Pokemon pokemon)
    {
        if (Quantity > 0)
        {
            if (pokemon.FueraDeCombate == true)
            {
                pokemon.FueraDeCombate = false;
                pokemon.Health = HpRecovered;
                Console.WriteLine($"El pokemon {pokemon.Name} a sido revivido con un {ItemsName} y recuperado {HpRecovered} HP.");
                Consume();
            }
            else
            {
                Console.WriteLine($"\n El pokemon {pokemon.Name} no está fuera de combate, no se necesita usar {ItemsName}.\n");
            }
        }
        else
        {
            Console.WriteLine($"\n La {ItemsName} no se puede usar, no quedan más unidades.\n");
        }
    }
}