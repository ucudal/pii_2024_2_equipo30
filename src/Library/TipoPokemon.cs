namespace Library;

public class TipoPokemon // Se podrá modificar al utilizar la API
{
    public static Tipo Fuego = CrearTipoFuego();
    public static Tipo Agua = CrearTipoAgua();
    public static Tipo Electrico = CrearTipoElectrico();
    public static Tipo Planta = CrearTipoPlanta();
    public static Tipo Fantasma = CrearTipoFantasma();
    public static Tipo Normal = CrearTipoNormal();
    public static Tipo Lucha = CrearTipoLucha();
    public static Tipo Psiquico = CrearTipoPsicologico();
    public static Tipo Hielo = CrearTipoHielo();
    public static Tipo Volador = CrearTipoVolador();
    public static Tipo Dragón = CrearTipoDragon();
    public static Tipo Oscuro = CrearTipoOscuro();
    public static Tipo Roca = CrearTipoRoca();

    private static Tipo CrearTipoFuego()
    {
        var tipoFuego = new Tipo("Fuego");
        tipoFuego.AgregarEfectividad("Agua", 0.5); // Fuego es débil ante el Agua
        tipoFuego.AgregarEfectividad("Planta", 2.0); // Fuego es fuerte contra Planta
        return tipoFuego;
    }

    private static Tipo CrearTipoAgua()
    {
        var tipoAgua = new Tipo("Agua");
        tipoAgua.AgregarEfectividad("Fuego", 2.0); // Agua es fuerte contra el Fuego
        tipoAgua.AgregarEfectividad("Electrico", 0.5); // Agua es débil ante Eléctrico
        return tipoAgua;
    }

    private static Tipo CrearTipoElectrico()
    {
        var tipoElectrico = new Tipo("Electrico");
        tipoElectrico.AgregarEfectividad("Agua", 2.0); // Eléctrico es fuerte contra el Agua
        return tipoElectrico;
    }

    private static Tipo CrearTipoPlanta()
    {
        var tipoPlanta = new Tipo("Planta");
        tipoPlanta.AgregarEfectividad("Fuego", 0.5); // Planta es débil ante el Fuego
        tipoPlanta.AgregarEfectividad("Agua", 2.0); // Planta es fuerte contra el Agua
        return tipoPlanta;
    }

    private static Tipo CrearTipoFantasma()
    {
        var tipoFantasma = new Tipo("Fantasma");
        tipoFantasma.AgregarEfectividad("Lucha", 0.5); // Fantasma es débil ante Lucha
        tipoFantasma.AgregarEfectividad("Normal", 0.0); // Fantasma es inmune a Normal
        return tipoFantasma;
    }

    private static Tipo CrearTipoNormal()
    {
        return new Tipo("Normal");
    }

    private static Tipo CrearTipoLucha()
    {
        var tipoLucha = new Tipo("Lucha");
        tipoLucha.AgregarEfectividad("Normal", 2.0); // Lucha es fuerte contra Normal
        return tipoLucha;
    }

    private static Tipo CrearTipoPsicologico()
    {
        return new Tipo("Psíquico");
    }

    private static Tipo CrearTipoHielo()
    {
        var tipoHielo = new Tipo("Hielo");
        tipoHielo.AgregarEfectividad("Fuego", 0.5); // Hielo es débil ante el Fuego
        return tipoHielo;
    }

    private static Tipo CrearTipoVolador()
    {
        var tipoVolador = new Tipo("Volador");
        tipoVolador.AgregarEfectividad("Lucha", 2.0); // Volador es fuerte contra Lucha
        return tipoVolador;
    }

    private static Tipo CrearTipoDragon()
    {
        return new Tipo("Dragón");
    }

    private static Tipo CrearTipoOscuro()
    {
        return new Tipo("Oscuro");
    }

    private static Tipo CrearTipoRoca()
    {
        return new Tipo("Roca");
    }
}
