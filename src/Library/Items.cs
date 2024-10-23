namespace Library;

//clase interface echa para seguir la guia de diseño y que mantenga un bajo acoplamiento
public interface IItem
{
    int VidaMax { get; set; }
    string ItemsName { get; set; }
    string ItemsDescription { get; set; }
    int Quantity { get; set; }
    
    void Use(Pokemon pokemon);//para aplicar los items se necesita usar el metodo use sino no surge efecto segun la guia de diseño
    void Consume();
}

public abstract class Items : IItem //clase abstracta que implementa interfaz
{
    public int VidaMax { get; set; }
    public string ItemsName { get; set; }
    public string ItemsDescription { get; set; }
    public int Quantity { get; set; }

    //constructor creado para cumplir con creator, por responsabilidad de los items y sus instancias
    public Items(string itemsName, string itemsDescription, int quantity)
    {
        ItemsName = itemsName;
        ItemsDescription = itemsDescription;
        Quantity = quantity;
    }

    public abstract void Use(Pokemon pokemon);
    
    public void Consume() //metodo para reducir la cantidad de items y indica al jugador(entrenador)
    {
        if (Quantity > 0)
        {
            Quantity--;
            Console.WriteLine($"{ItemsName} a sido utilizado. Te quedan {Quantity} restantes.");
        }
        else
        {
            Console.WriteLine($"Ya no te quedan mas {ItemsName}.");
        }
    }
}

//pocion que cura gran cantidad de vida (segun la guia de usuario tendria que ser 70 HP)
//clase que hereda de items
public class SuperPotion : Items
{
    private int HpRecovered;

    public SuperPotion(int quantity, int hpRecovered) : base("SuperPoción","Poción mejorada, puede curar mas que una poción normal", quantity)
    {
        HpRecovered = hpRecovered;
    }
    
    public override void Use(Pokemon pokemon)
    {
        pokemon.Vida += HpRecovered;
        Consume();
        Console.WriteLine($" El Pokemon {pokemon.Nombre} ha recuperado {HpRecovered} puntos de salud.");
        Consume();
    }
}

//cura total:Cura a un Pokémon de efectos de ataques especiales(dormido, paralizado, envenenado, o quemado.)
//clase que hereda de items

public class TotalCure : Items
{
    public TotalCure(int quantity) : base("Cura total", "Cura a un Pokémon de efectos de ataques especiales(dormido, paralizado, envenenado, o quemado.)", quantity)
    {

    }

    public override void Use(Pokemon pokemon)
    {
        if (Quantity > 0)
        {
            if (pokemon.Estado == EstadoEspecial.Envenenado)
            {
                pokemon.Estado = EstadoEspecial.Ninguno;
                Console.WriteLine($"El pokemon {pokemon.Nombre} ya no está envenenado.");
            }
            if (pokemon.Estado == EstadoEspecial.Paralizado)
            {
                pokemon.Estado = EstadoEspecial.Ninguno;
                Console.WriteLine($"El pokemon {pokemon.Nombre} ya no está paralizado.");
            }

            if (pokemon.Estado == EstadoEspecial.Quemado)
            {
                pokemon.Estado = EstadoEspecial.Ninguno;
                Console.WriteLine($"El pokemon {pokemon.Nombre} ya no está quemado.");
            }

            Consume();
        }
        else
        {
            Console.WriteLine($"La cura {ItemsName} no se puede usar, no hay mas");
        }
    }
}

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
            if (pokemon.FueraDeCombate = true)
            {
                pokemon.FueraDeCombate = false;
                pokemon.Vida = HpRecovered;
                Console.WriteLine($"El pokemon {pokemon.Nombre} a sido revivido con un {ItemsName} y recuperado {HpRecovered} HP.");
                Consume();
            }
        }
        else
        {
            Console.WriteLine($"La cura {ItemsName} no se puede usar, no hay mas");
        }
    }
}