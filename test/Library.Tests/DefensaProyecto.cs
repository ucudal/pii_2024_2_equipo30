namespace Library.Tests;

public class DefensaProyecto_Test
{
    [SetUp]
    public void setup()
    {
        
    }

    [Test]
    public void DefensaTest()
    {
        List<Pokemon> listaPokemon = new List<Pokemon>();
        Type type = new Type("grass");
        type.SetType("grass");
        Type type2 = new Type("electric");
        type2.SetType("electric");
        Type type3 = new Type("fire");
        type3.SetType("fire");
        Type type4 = new Type("water");
        type4.SetType("water");
        Type type5 = new Type("normal");
        type5.SetType("normal");
    
        listaPokemon.Add( new Pokemon("Pikachu", 35, 100, 55, 40, 90, 50, type2, new List<Move> { new Move() }));
        listaPokemon.Add(new Pokemon("Charizard", 78, 150, 84, 78, 100, 85,type3,new List<Move> { new Move(SpecialStatus.NoneStatus) }));
        listaPokemon.Add(new Pokemon("Bulbasaur", 45, 100, 49, 49, 45, 60, type, new List<Move> { new Move() }));
        listaPokemon.Add(new Pokemon("Squirtle", 44, 100, 48, 65, 43, 50,type4, new List<Move> { new Move() }));
        listaPokemon.Add(new Pokemon("Jigglypuff", 115, 100, 45, 20, 20, 25, type5, new List<Move> { new Move() }));
        listaPokemon.Add(new Pokemon("Gengar", 60, 100, 65, 60, 110, 75, type5, new List<Move> { new Move() }));
        Player jugadoractual = new Player(null, "jugador1", listaPokemon);
        Player jugadorenemigo = new Player(null, "JugadorEnemigo", listaPokemon);
        jugadorenemigo.actualPokemon = listaPokemon[2];
        Pokemon pokemonefectivo = listaPokemon[1];
        Battle batalla = new Battle(jugadoractual, jugadorenemigo);
        Assert.That(batalla.CalculateProbabilityOfWinning(jugadoractual,jugadoractual),Is.EqualTo(pokemonefectivo));
    }

    

}