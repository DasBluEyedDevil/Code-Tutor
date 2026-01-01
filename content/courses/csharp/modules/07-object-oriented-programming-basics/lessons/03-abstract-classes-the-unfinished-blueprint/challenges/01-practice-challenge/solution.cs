abstract class Employee
{
    public string Name;
    public int ID;
    
    public abstract decimal CalculateSalary();
    
    public void Display()
    {
        Console.WriteLine(Name + " (ID: " + ID + ")");
        Console.WriteLine("Salary: $" + CalculateSalary());
    }
}

class HourlyEmployee : Employee
{
    public decimal HourlyRate;
    public int HoursWorked;
    
    public override decimal CalculateSalary()
    {
        return HourlyRate * HoursWorked;
    }
}

class SalariedEmployee : Employee
{
    public decimal AnnualSalary;
    
    public override decimal CalculateSalary()
    {
        return AnnualSalary / 12;
    }
}

HourlyEmployee emp1 = new HourlyEmployee();
emp1.Name = "John";
emp1.ID = 101;
emp1.HourlyRate = 25;
emp1.HoursWorked = 160;
emp1.Display();

SalariedEmployee emp2 = new SalariedEmployee();
emp2.Name = "Jane";
emp2.ID = 102;
emp2.AnnualSalary = 60000;
emp2.Display();