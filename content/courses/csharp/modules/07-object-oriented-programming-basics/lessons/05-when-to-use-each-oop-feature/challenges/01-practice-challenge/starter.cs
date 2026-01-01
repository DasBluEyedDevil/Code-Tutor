interface IFeedable
{
    void Feed();
}

abstract class Animal
{
    public string Name;
    public int Age;
    
    public abstract void MakeSound();
}

// Implement Lion and Penguin

// Create arrays and demonstrate polymorphism