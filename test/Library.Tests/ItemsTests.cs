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
        _pokemon.VidaMax = 100;
    }  

    [Test]
    public void SuperPotion_Heal()
    {
        
        
        //Asserts
        
    }
    
    [Test]
    public void SuperPotion_dontHeal()
    {
        
        
        //Asserts
        
        
        
    }
    
    [Test]
    public void TotalCure_Cleanstates()
    {
        
        //Asserts
        
    }
    
    [Test]
    public void TotalCure_dontcleanstates()
    {
        
        
        //Asserts
        
        
    }
    
    [Test] 
    public void Revive_CanRevive()
    {
        
        
        //Asserts
        
            
    }
     
    [Test] 
    public void Revive_Leavedead()
    {
        
        //Asserts
        
    }
    
}
