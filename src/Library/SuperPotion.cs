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
        double newHealth = pokemon.Health + HpRecovered;
        
        if (newHealth > pokemon.MaxHealt)
        {
            pokemon.Health = pokemon.MaxHealt;
        }
        else
        {
            pokemon.Health = newHealth;
        }
        Consume();
        Console.WriteLine($"\n El Pokemon {pokemon.Name} ha recuperado {HpRecovered} puntos de salud. Ahora tiene {pokemon.Health:F1}/{pokemon.MaxHealt} puntos de vida.\n");
    }
}