using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Library;
using NUnit.Framework;
namespace Library.Tests;

public class ItemsTests
{
    private Pokemon _pokemon;
    private SuperPotion _superPotion;
    private TotalCure _totalCure;
    private Revive _revive;
    
    [SetUp]
    public void Setup()
    {
        _pokemon = new Pokemon("Pikachu", 1, 100, 55, 40, 50, 50, new Type(), new List<Move>());
        _pokemon.VidaMax = 200;
    }  

    [Test]
    public void SuperPotion_Heal()
    {
        //Asserts

        _pokemon.VidaMax = 100;
        _pokemon.Health = 50;

        var superPotion = new SuperPotion(quantity: 1, hpRecovered: 60);
        
        //act
        superPotion.Use(_pokemon);
        
        Assert.That(_pokemon.Health, Is.EqualTo(100));

    }



    [Test]
    public void TotalCure_poison()
    {
        //Arrange
        _pokemon.Estado = EstadoEspecial.Envenenado;
        var totalCure = new TotalCure(quantity: 1);
        //act
        totalCure.Use(_pokemon);
        
        //Assert
        Assert.That(_pokemon.Estado, Is.EqualTo(EstadoEspecial.Ninguno));

    }
    [Test]
    public void TotalCure_burned()
    {
        //Arrange
        _pokemon.Estado = EstadoEspecial.Quemado;
        var totalCure = new TotalCure(quantity: 1);
        //act
        totalCure.Use(_pokemon);
        
        //Assert
        Assert.That(_pokemon.Estado, Is.EqualTo(EstadoEspecial.Ninguno));

    }
    [Test]
    public void TotalCure_Paralized()
    {
        //Arrange
        _pokemon.Estado = EstadoEspecial.Paralizado;
        var totalCure = new TotalCure(quantity: 1);
        //act
        totalCure.Use(_pokemon);
        
        //Assert
        Assert.That(_pokemon.Estado, Is.EqualTo(EstadoEspecial.Ninguno));

    }
    [Test]
    public void TotalCure_Dormido()
    {
        //Arrange
        _pokemon.Estado = EstadoEspecial.Dormido;
        var totalCure = new TotalCure(quantity: 1);
        //act
        totalCure.Use(_pokemon);
        
        //Assert
        Assert.That(_pokemon.Estado, Is.EqualTo(EstadoEspecial.Dormido));

    }
    
    
    [Test] 
    public void Revive_CanRevive()
    {
        //arrange
        _pokemon.VidaMax = 200;
        _pokemon.FueraDeCombate = true;
        _pokemon.Health = 0;
        var revive = new Revive(quantity: 1);
        
        //act
        revive.Use(_pokemon);

        //Assert
        Assert.That(_pokemon.FueraDeCombate, Is.False);
        Assert.That(_pokemon.Health, Is.EqualTo(100));

    }
     
    [Test] 
    public void Revive_isnotdead()
    {
        //arrange
        _pokemon.VidaMax = 200;
        _pokemon.FueraDeCombate = false;
        
        
        var revive = new Revive(quantity: 1);
        Assert.That(_pokemon.FueraDeCombate, Is.False);
        Assert.That(100, Is.EqualTo(_pokemon.Health));
        Assert.That(revive.Quantity, Is.EqualTo(1));
    }
    
}
