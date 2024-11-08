﻿namespace Library;

//clase interface hecha para seguir la guia de diseño y que mantenga un bajo acoplamiento
public interface IItem
{
    int VidaMax { get; set; }
    string ItemsName { get; set; }
    string ItemsDescription { get; set; }
    int Quantity { get; set; }
    
    void Use(Pokemon pokemon); //para aplicar los items se necesita usar el metodo use sino no surge efecto según la guia de diseño
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
            Console.WriteLine($"\n {ItemsName} ha sido utilizado. Te quedan {Quantity} restantes.\n");
        }
        else
        {
            Console.WriteLine($"\n Ya no te quedan más {ItemsName}.\n");
        }
    }
}

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
        double nuevaVida = pokemon.Health + HpRecovered;
        
        if (nuevaVida > pokemon.VidaMax)
        {
            pokemon.Health = pokemon.VidaMax;
        }
        else
        {
            pokemon.Health = nuevaVida;
        }
        Consume();
        Console.WriteLine($"\n El Pokemon {pokemon.Name} ha recuperado {HpRecovered} puntos de salud. Ahora tiene {pokemon.Health:F1}/{pokemon.VidaMax} puntos de vida.\n");
    }
}

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
