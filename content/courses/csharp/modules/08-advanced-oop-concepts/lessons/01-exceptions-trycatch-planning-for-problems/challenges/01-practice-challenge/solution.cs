try
{
    Console.WriteLine("Enter first number:");
    int num1 = int.Parse(Console.ReadLine());
    
    Console.WriteLine("Enter second number:");
    int num2 = int.Parse(Console.ReadLine());
    
    Console.WriteLine("Enter operation (+, -, *, /):");
    string op = Console.ReadLine();
    
    int result = 0;
    if (op == "+") result = num1 + num2;
    else if (op == "-") result = num1 - num2;
    else if (op == "*") result = num1 * num2;
    else if (op == "/") result = num1 / num2;
    
    Console.WriteLine("Result: " + result);
}
catch (FormatException ex)
{
    Console.WriteLine("Error: Please enter valid numbers only!");
}
catch (DivideByZeroException ex)
{
    Console.WriteLine("Error: Cannot divide by zero!");
}
catch (Exception ex)
{
    Console.WriteLine("Unexpected error: " + ex.Message);
}