class Character
{
    public string Name;
    public int Health = 100;
    public int Mana = 50;
    
    public int Attack()
    {
        int damage = new Random().Next(10, 21);
        Console.WriteLine(Name + " attacks for " + damage + " damage!");
        return damage;
    }
    
    public void Heal(int amount)
    {
        Health += amount;
        if (Health > 100) Health = 100;
        Console.WriteLine(Name + " healed for " + amount + ". Health: " + Health);
    }
    
    public bool CastSpell()
    {
        if (Mana >= 20)
        {
            Mana -= 20;
            Console.WriteLine(Name + " casts spell! Mana: " + Mana);
            return true;
        }
        Console.WriteLine("Not enough mana!");
        return false;
    }
}

Character hero = new Character();
hero.Name = "Warrior";
int dmg = hero.Attack();
hero.Heal(20);
bool cast = hero.CastSpell();
Console.WriteLine("Spell cast success: " + cast);