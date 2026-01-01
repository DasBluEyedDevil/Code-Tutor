interface IFeedable
{
    void Feed();
}

abstract class Animal
{
    public string Name;
    public int Age;
    
    public abstract void MakeSound();
    
    public void DisplayInfo()
    {
        Console.WriteLine(Name + ", age " + Age);
    }
}

class Lion : Animal, IFeedable
{
    public override void MakeSound()
    {
        Console.WriteLine("Roar!");
    }
    
    public void Feed()
    {
        Console.WriteLine("Feeding " + Name + " meat");
    }
}

class Penguin : Animal, IFeedable
{
    public override void MakeSound()
    {
        Console.WriteLine("Squawk!");
    }
    
    public void Feed()
    {
        Console.WriteLine("Feeding " + Name + " fish");
    }
}

Animal[] animals = { 
    new Lion { Name = "Simba", Age = 5 },
    new Penguin { Name = "Pingu", Age = 2 }
};

foreach (Animal a in animals)
{
    a.DisplayInfo();
    a.MakeSound();
}

IFeedable[] feedables = { new Lion(), new Penguin() };
foreach (IFeedable f in feedables)
{
    f.Feed();
}