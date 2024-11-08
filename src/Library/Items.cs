using Library;

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






