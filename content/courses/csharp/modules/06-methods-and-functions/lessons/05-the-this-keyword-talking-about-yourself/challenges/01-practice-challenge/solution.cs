class Employee
{
    private string name;
    private decimal salary;
    
    public Employee(string name, decimal salary)
    {
        this.name = name;
        this.salary = salary;
    }
    
    public void GiveRaise(decimal amount)
    {
        this.salary += amount;
        Console.WriteLine(this.name + " got a raise! New salary: $" + this.salary);
    }
    
    public bool CompareSalary(Employee other)
    {
        return this.salary > other.salary;
    }
    
    public void Display()
    {
        Console.WriteLine(this.name + ": $" + this.salary);
    }
}

Employee emp1 = new Employee("Alice", 50000);
Employee emp2 = new Employee("Bob", 60000);
emp1.Display();
emp2.Display();
emp1.GiveRaise(15000);
if (emp1.CompareSalary(emp2))
    Console.WriteLine("Alice earns more!");
else
    Console.WriteLine("Bob earns more!");