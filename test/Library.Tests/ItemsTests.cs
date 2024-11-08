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
        _pokemon.MaxHealt = 200;
    }  

    [Test]
    public void SuperPotion_Heal()
    {
        //Asserts

        _pokemon.MaxHealt = 100;
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
        _pokemon.Status = EspecialStatus.Poisoned;
        var totalCure = new TotalCure(quantity: 1);
        //act
        totalCure.Use(_pokemon);
        
        //Assert
        Assert.That(_pokemon.Status, Is.EqualTo(EspecialStatus.NoneStatus));

    }
    [Test]
    public void TotalCure_burned()
    {
        //Arrange
        _pokemon.Status = EspecialStatus.Burned;
        var totalCure = new TotalCure(quantity: 1);
        //act
        totalCure.Use(_pokemon);
        
        //Assert
        Assert.That(_pokemon.Status, Is.EqualTo(EspecialStatus.NoneStatus));

    }
    [Test]
    public void TotalCure_Paralized()
    {
        //Arrange
        _pokemon.Status = EspecialStatus.Paralyzed;
        var totalCure = new TotalCure(quantity: 1);
        //act
        totalCure.Use(_pokemon);
        
        //Assert
        Assert.That(_pokemon.Status, Is.EqualTo(EspecialStatus.NoneStatus));

    }
    [Test]
    public void TotalCure_Dormido()
    {
        //Arrange
        _pokemon.Status = EspecialStatus.Asleep;
        var totalCure = new TotalCure(quantity: 1);
        //act
        totalCure.Use(_pokemon);
        
        //Assert
        Assert.That(_pokemon.Status, Is.EqualTo(EspecialStatus.Asleep));

    }
    
    
    [Test] 
    public void Revive_CanRevive()
    {
        //arrange
        _pokemon.MaxHealt = 200;
        _pokemon.Outofaction = true;
        _pokemon.Health = 0;
        var revive = new Revive(quantity: 1);
        
        //act
        revive.Use(_pokemon);

        //Assert
        Assert.That(_pokemon.Outofaction, Is.False);
        Assert.That(_pokemon.Health, Is.EqualTo(100));

    }
     
    [Test] 
    public void Revive_isnotdead()
    {
        //arrange
        _pokemon.MaxHealt = 200;
        _pokemon.Outofaction = false;
        
        
        var revive = new Revive(quantity: 1);
        Assert.That(_pokemon.Outofaction, Is.False);
        Assert.That(100, Is.EqualTo(_pokemon.Health));
        Assert.That(revive.Quantity, Is.EqualTo(1));
    }
    
}
