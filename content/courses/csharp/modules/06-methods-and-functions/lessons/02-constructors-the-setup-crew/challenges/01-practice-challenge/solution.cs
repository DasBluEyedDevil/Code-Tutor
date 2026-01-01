// Car class with constructor
class Car
{
    public string Brand;
    public string Model;
    public int Year;
    
    // Constructor
    public Car(string brand, string model, int year)
    {
        Brand = brand;
        Model = model;
        Year = year;
        Console.WriteLine("Car created: " + Brand + " " + Model);
    }
}

// Create cars using constructor
Car car1 = new Car("Toyota", "Camry", 2020);
Car car2 = new Car("Honda", "Civic", 2019);

// Display cars
Console.WriteLine(car1.Brand + " " + car1.Model + " (" + car1.Year + ")");
Console.WriteLine(car2.Brand + " " + car2.Model + " (" + car2.Year + ")");