using System.ComponentModel.DataAnnotations.Schema;

namespace Library;

public class Pokemon
{
    public string Nombre { get; set; }
    public int Vida { get; set; }
    public int Id { get; set; }  // No la utlizamos hasta que implementemos el uso de la API
    public List<Movimiento> Movimientos { get; set; }
    public Tipo TipoPokemon { get; set; }

    public Pokemon(string nombre, int vida, List<Movimiento> movimientos, Tipo tipoPokemon)
    {
        Nombre = nombre;
        Vida = vida;
        Movimientos = movimientos;
        TipoPokemon = tipoPokemon;
    }
    public void Atacar(Pokemon oponente, Movimiento movimiento)
    {
        double modificador = 1.0;

        if (TipoPokemon.Efectividad.ContainsKey(oponente.TipoPokemon.Nombre))
        {
            modificador = TipoPokemon.Efectividad[oponente.TipoPokemon.Nombre];
        }

        int daño = (int)(movimiento.Poder * modificador);
        oponente.Vida -= movimiento.Poder;
        
        if (oponente.Vida < 0)
        {
            oponente.Vida = 0;  // La vida no puede ser negativa
        }
        Console.WriteLine($"{Nombre} usó {movimiento.Nombre}! {oponente.Nombre} ahora tiene {oponente.Vida} puntos de vida.");
    }

    public bool EstaFueraDeCombate()
    {
        return Vida <= 0;
    }
}

