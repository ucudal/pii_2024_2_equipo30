namespace Library;

public class Movimientos
{
    public List<Movimiento> ListaMovimientos { get; set; }

    public Movimientos()
    {
        ListaMovimientos = new List<Movimiento>
        {
            new Movimiento("Impactrueno", 40),
            new Movimiento("Llamarada", 90),
            new Movimiento("Golpe Karate", 50),
            new Movimiento("Rayo Hielo", 80),
            new Movimiento("Placaje", 40),
            new Movimiento("Hoja Afilada", 70),
            new Movimiento("Terremoto", 100),
            new Movimiento("Pistola Agua", 40),
            new Movimiento("Surf", 90),
            new Movimiento("Hidrobomba", 110),
            new Movimiento("Viento Plata", 60),
            new Movimiento("Látigo", 30),
            new Movimiento("Colmillo Ígneo", 65),
            new Movimiento("Bola Sombra", 80),
            new Movimiento("Pulso Dragón", 85)
        };
    }
    
    // Método para obtener 4 movimientos aleatorios
    public List<Movimiento> ObtenerMovimientosAleatorios()
    {
        Random random = new Random();
        List<Movimiento> movimientosAleatorios = new List<Movimiento>();

        while (movimientosAleatorios.Count < 4)
        {
            int index = random.Next(ListaMovimientos.Count);
            Movimiento movimientoSeleccionado = ListaMovimientos[index];

            if (!movimientosAleatorios.Contains(movimientoSeleccionado))
            {
                movimientosAleatorios.Add(movimientoSeleccionado);
            }
        }

        return movimientosAleatorios;
    }
}
