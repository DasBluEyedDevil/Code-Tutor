// Create your Car class here
class Car
{
    public string Brand;
    public string Model;
    public int Year;
}

// Create two Car objects and display them
Car car1 = new Car();
car1.Brand = "Toyota";
car1.Model = "Camry";
car1.Year = 2020;

Car car2 = new Car();
car2.Brand = "Honda";
car2.Model = "Civic";
car2.Year = 2019;

Console.WriteLine(car1.Brand + " " + car1.Model + " (" + car1.Year + ")");
Console.WriteLine(car2.Brand + " " + car2.Model + " (" + car2.Year + ")");