class Vehicle
{
    public static int TotalVehicles = 0;
    public static int MaxSpeed = 0;
    
    public string Model;
    public int Speed;
    
    public Vehicle(string model)
    {
        Model = model;
        Speed = 0;
        TotalVehicles++;
    }
    
    public void Accelerate(int amount)
    {
        Speed += amount;
        Console.WriteLine(Model + " accelerated to " + Speed + " mph");
        
        if (Speed > MaxSpeed)
            MaxSpeed = Speed;
    }
    
    public static void ShowStats()
    {
        Console.WriteLine("=== Vehicle Stats ===");
        Console.WriteLine("Total Vehicles: " + TotalVehicles);
        Console.WriteLine("Max Speed Reached: " + MaxSpeed + " mph");
    }
}

Vehicle car1 = new Vehicle("Tesla");
Vehicle car2 = new Vehicle("BMW");
Vehicle car3 = new Vehicle("Audi");

car1.Accelerate(80);
car2.Accelerate(120);
car3.Accelerate(95);

Vehicle.ShowStats();