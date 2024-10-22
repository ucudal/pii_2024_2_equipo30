using System.ComponentModel.DataAnnotations.Schema;

namespace Library;

public class Pokemon
{
    public string Nombre { get; set; }
    public int Vida { get; set; }
    public int Id { get; set; }  // No la utlizamos hasta que implementemos el uso de la API
    public List<Movimiento> Movimientos { get; set; }
    public Tipo TipoPokemon { get; set; }
    public EstadoEspecial Estado { get; set; }
    public int TurnosDormido { get; set; } = 0;
    public int TurnosRestantesDeSueño { get; set; } = 0;
    private Random random = new Random();

    public Pokemon(string nombre, int vida, List<Movimiento> movimientos, Tipo tipoPokemon)
    {
        Nombre = nombre;
        Vida = vida;
        Movimientos = movimientos;
        TipoPokemon = tipoPokemon;
    }
    //Atencion, la clase atacar actualmente se encarga de manejar la efectividad y los Ataques especiales
    public void Atacar(Pokemon oponente, Movimiento movimiento)
    {
        // Verificar si el Pokémon actual puede atacar
        if (this.Estado == EstadoEspecial.Dormido)
        {
            Console.WriteLine($"{Nombre} está dormido y no puede atacar este turno.");
            return; 
        }
        else if (this.Estado == EstadoEspecial.Paralizado && new Random().Next(0, 2) == 0)
        {
            Console.WriteLine($"{Nombre} está paralizado y no puede atacar este turno.");
            return;
        }

        Console.WriteLine($"{Nombre} usa {movimiento.Nombre}!");
        double modificador = 1.0;
        
        if (TipoPokemon.Efectividad.ContainsKey(oponente.TipoPokemon.Nombre))
        {
            modificador = TipoPokemon.Efectividad[oponente.TipoPokemon.Nombre];
        }
        if (movimiento.EstadoEspecial != EstadoEspecial.Ninguno && oponente.Estado == EstadoEspecial.Ninguno)
        {
            oponente.Estado = movimiento.EstadoEspecial;
            Console.WriteLine($"{oponente.Nombre} ahora está {movimiento.EstadoEspecial}.");
        }
        else if (movimiento.EstadoEspecial == EstadoEspecial.Dormido && oponente.Estado == EstadoEspecial.Ninguno)
        {
            oponente.Dormir(); 
        }
        else if (movimiento.EstadoEspecial == EstadoEspecial.Paralizado && oponente.Estado == EstadoEspecial.Ninguno)
        {
            oponente.Paralizar();
        }
        else if (oponente.Estado != EstadoEspecial.Ninguno)
        {
            Console.WriteLine($"{oponente.Nombre} ya está afectado por {oponente.Estado}, no se puede aplicar otro estado.");
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

    public bool PuedeAtacar()
    {
        if (Estado == EstadoEspecial.Dormido && TurnosRestantesDeSueño > 0)
        {
            TurnosRestantesDeSueño--;
            Console.WriteLine($"{Nombre} está dormido y no puede atacar. Turnos restantes: {TurnosRestantesDeSueño}");
            if (TurnosRestantesDeSueño == 0)
            {
                Estado = EstadoEspecial.Ninguno;
                Console.WriteLine($"{Nombre} se ha despertado.");
            }

            return false;
        }

        // Verificar si está paralizado
        if (Estado == EstadoEspecial.Paralizado)
        {
            // 50% de probabilidad de atacar
            bool puedeAtacar = random.Next(0, 2) == 0; // Devuelve true o false aleatoriamente
            if (!puedeAtacar)
            {
                Console.WriteLine($"{Nombre} está paralizado y no puede atacar este turno.");
                return false;
            }
        }

        return true;
    }

    public void ProcesarEstado()
    {
        if (Estado == EstadoEspecial.Envenenado)
        {
            int damage = (int)(Vida * 0.05);
            Vida -= damage;
            Console.WriteLine($"{Nombre} está envenenado y pierde {damage} puntos de vida.");
        }
        else if (Estado == EstadoEspecial.Quemado)
        {
            int burnDamage = (int)(Vida * 0.10);
            Vida -= burnDamage;
            Console.WriteLine($"{Nombre} está quemado y pierde {burnDamage} puntos de vida.");
        }
        else if (Estado == EstadoEspecial.Dormido)
        {
            Console.WriteLine($"{Nombre} está dormido, no puede atacar");
        }
    }
    public void Dormir()
    {
        Estado = EstadoEspecial.Dormido;
        Random random = new Random();
        TurnosDormido = random.Next(1, 5); // Aleatoreamente va de 1 a 4
        TurnosRestantesDeSueño = TurnosDormido;
        Console.WriteLine($"{Nombre} ha sido dormido y estará dormido por {TurnosDormido} turnos.");
    }
    public void Paralizar()
    {
        Estado = EstadoEspecial.Paralizado;
        Console.WriteLine($"{Nombre} ha sido paralizado.");
    }
}

