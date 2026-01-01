// Create Vehicle with primary constructor
public class Vehicle(string make, string model, int year)
{
    public string GetInfo()
    {
        return $"{year} {make} {model}";
    }
}

// Create Car that inherits from Vehicle
public class Car(string make, string model, int year, int doors)
    : Vehicle(make, model, year)
{
    public void Describe()
    {
        Console.WriteLine($"{GetInfo()} with {doors} doors");
    }
}

// Test your classes
var vehicle = new Vehicle("Honda", "Civic", 2024);
Console.WriteLine(vehicle.GetInfo());

var car = new Car("Toyota", "Camry", 2023, 4);
car.Describe();