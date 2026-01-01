class Character
{
    public string Name;
    public int Health = 100;
    public int Mana = 50;
    
    // Add Attack method (returns int damage 10-20)
    // Hint: Use new Random().Next(10, 21) for random 10-20
    
    // Add Heal method (void, takes int amount)
    
    // Add CastSpell method (returns bool)
}

// Create character and test methods
Character hero = new Character();
hero.Name = "Warrior";
// Call Attack, Heal(20), CastSpell
// Display results