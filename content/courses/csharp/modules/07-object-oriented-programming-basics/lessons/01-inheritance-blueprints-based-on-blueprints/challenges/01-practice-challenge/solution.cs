class Animal
{
    public string Name;
    public int Age;
    
    public void Eat()
    {
        Console.WriteLine(Name + " is eating");
    }
    
    public void Sleep()
    {
        Console.WriteLine(Name + " is sleeping");
    }
}

class Dog : Animal
{
    public string Breed;
    
    public void Bark()
    {
        Console.WriteLine("Woof!");
    }
}

class Cat : Animal
{
    public string Color;
    
    public void Meow()
    {
        Console.WriteLine("Meow!");
    }
}

Dog dog = new Dog();
dog.Name = "Buddy";
dog.Age = 3;
dog.Breed = "Golden Retriever";
dog.Eat();    // Inherited!
dog.Bark();   // Dog's own

Cat cat = new Cat();
cat.Name = "Whiskers";
cat.Age = 2;
cat.Color = "Orange";
cat.Eat();    // Inherited!
cat.Meow();   // Cat's own