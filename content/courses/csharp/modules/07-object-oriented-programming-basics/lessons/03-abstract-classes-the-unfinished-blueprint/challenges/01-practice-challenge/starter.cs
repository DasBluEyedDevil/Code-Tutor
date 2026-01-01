abstract class Employee
{
    public string Name;
    public int ID;
    
    // Abstract method
    
    // Regular method
    public void Display()
    {
        Console.WriteLine(Name + " (ID: " + ID + ")");
        Console.WriteLine("Salary: $" + CalculateSalary());
    }
}

class HourlyEmployee : Employee
{
    // Properties and override
}

class SalariedEmployee : Employee
{
    // Properties and override
}