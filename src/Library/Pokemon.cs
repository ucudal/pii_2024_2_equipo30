using System.ComponentModel.DataAnnotations.Schema;

namespace Library;

public class Pokemon
{
    public string Nombre;
    public string Tipo;
    public int Vida;
    private List<Estados> EstadosList;
    public List<Resistencias> ResistenciasList;

    public Pokemon(string nombre)
    {
        string Nombre = nombre;
        string Tipo = this.Tipo;
        int Vida = this.Vida;
        EstadosList = new List<Estados>();
    }

    public void AtaqueNormal(Ataque ataque)
    {

    }

    public void AtaqueEspecial(AtaqueEspecial ataque)
    {
        
    }



}
