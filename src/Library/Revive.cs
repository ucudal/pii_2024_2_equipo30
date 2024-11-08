namespace Library;


//Revivir: Revive a un Pokémon con el 50% de su HP total.
//clase que hereda de items
public class Revive : Items
{
    public Revive(int quantity) : base("Revivir", "Revive a un Pokémon con el 50% de su HP total.", quantity)
    {
    }
    
    public override void Use(Pokemon pokemon)
    {
        if (Quantity > 0)
        {
            if (pokemon.EstaFueraDeCombate()) // Verifica si el Pokémon está fuera de combate
            {
                double HpRecovered = pokemon.VidaMax / 2; // Calcula el 50% de la vida máxima del Pokémon

                pokemon.Health = HpRecovered; // Recupera su salud
                pokemon.FueraDeCombate = false; // Asegúrate de que esté marcado como en combate

                // Ahora llamamos a EstaFueraDeCombate para asegurar que el estado se actualice correctamente
                pokemon.EstaFueraDeCombate();

                Console.WriteLine($"El Pokémon {pokemon.Name} ha sido revivido con un {ItemsName} y recuperado {HpRecovered} HP.");
                Consume(); // Consume el objeto Revive
            }
            else
            {
                Console.WriteLine($"\nEl Pokémon {pokemon.Name} no está fuera de combate, no se necesita usar {ItemsName}.\n");
            }
        }
        else
        {
            Console.WriteLine($"\nLa {ItemsName} no se puede usar, no quedan más unidades.\n");
        }
    }
}
