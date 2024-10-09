namespace Library;

public class CambioDeTruno
{
    public bool Turno { get; set; }

    public bool Cambio(bool turno)
    {
        Turno = turno;
        return turno;
    }
}