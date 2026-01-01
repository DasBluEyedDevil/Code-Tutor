try
{
    Console.WriteLine("Enter first number:");
    // Parse input
    
    Console.WriteLine("Enter second number:");
    // Parse input
    
    Console.WriteLine("Enter operation (+, -, *, /):");
    string op = Console.ReadLine();
    
    // Perform calculation
    int result = 0;
    
    Console.WriteLine("Result: " + result);
}
catch (FormatException ex)
{
    // Handle invalid number
}
catch (DivideByZeroException ex)
{
    // Handle division by zero
}
catch (Exception ex)
{
    // Handle any other error
}